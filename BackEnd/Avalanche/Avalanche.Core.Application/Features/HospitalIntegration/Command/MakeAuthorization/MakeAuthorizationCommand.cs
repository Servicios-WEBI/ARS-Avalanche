using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HospitalIntegration;
using Avalanche.Core.Application.Dtos.Notification;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Command.MakeAuthorization
{
    public class MakeAuthorizationCommand : IRequest<AuthorizationResponseDTO>
    {
        [SwaggerParameter(Description = "Tipo de documento del afiliado.")]
        public string? DocumentType { get; set; }

        [SwaggerParameter(Description = "Número del documento del afiliado.")]
        [Required(ErrorMessage = "Debe ingresar el número del documento.")]
        public string DocumentNumber { get; set; }

        [SwaggerParameter(Description = "Número de la póliza.")]
        public string? PolicyNumber { get; set; }

        [SwaggerParameter(Description = "Tipo de autorización.")]
        [Required(ErrorMessage = "Debe ingresar el tipo de autorización.")]
        public string AuthorizationType { get; set; }

        [SwaggerParameter(Description = "Id de la solicitud del hospital.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar el id de la solicitud del hospital.")]
        public int HospitalApplicationId { get; set; }

        [SwaggerParameter(Description = "Monto solicitado por el afiliado para esta autorización.")]
        [Required(ErrorMessage = "Debe ingresar el monto solicitado.")]
        public double ApplicationAmount { get; set; }

        [SwaggerParameter(Description = "Hospital o centro de salud donde se realiza la solicitud.")]
        [Required(ErrorMessage = "Debe ingresar el hospital.")]
        public string Hospital { get; set; }
    }

    public class MakeAuthorizationCommandHandler : IRequestHandler<MakeAuthorizationCommand, AuthorizationResponseDTO>
    {
        private readonly IAffiliateValidationService _validationService;
        private readonly IPlanCoverageRepository _planCoverageRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IAuthorizationTypeRepository _authorizationTypeRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IAnalystRepository _analystRepository;
        private readonly INotificationSender _notificationSender;
        private readonly IAccountService _accountService;
        private readonly ILogger<MakeAuthorizationCommandHandler> _logger;

        public MakeAuthorizationCommandHandler(IAffiliateValidationService validationService, IPlanCoverageRepository planCoverageRepository,
            IAuthorizationRepository authorizationRepository, IAuthorizationTypeRepository authorizationTypeRepository,
            IHospitalRepository hospitalRepository, IStatusRepository statusRepository, IAnalystRepository analystRepository,
            INotificationSender notificationSender, IAccountService accountService, ILogger<MakeAuthorizationCommandHandler> logger)
        {
            _validationService = validationService;
            _planCoverageRepository = planCoverageRepository;
            _authorizationRepository = authorizationRepository;
            _authorizationTypeRepository = authorizationTypeRepository;
            _hospitalRepository = hospitalRepository;
            _statusRepository = statusRepository;
            _analystRepository = analystRepository;
            _notificationSender = notificationSender;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<AuthorizationResponseDTO> Handle(MakeAuthorizationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AuthorizationResponseDTO response = new();

                var hospital = await _hospitalRepository.GetByPropertyAsync(h => h.Name == command.Hospital.ToUpper());

                if (hospital == null)
                {
                    response.Status = "Fallido";
                    response.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No se encontó ese hospital" }];
                    return response;
                }

                var type = await _authorizationTypeRepository.GetByPropertyAsync(a => a.Name == command.AuthorizationType.ToUpper());

                if (type == null)
                {
                    response.Status = "Revise el tipo de solicitud";
                    response.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "El tipo de solicitud no está disponible en nuestro sistema" }];
                    return response;
                }

                var validationResult = await _validationService.ValidateAsync(command.DocumentType, command.DocumentNumber, command.PolicyNumber);

                if (validationResult.Status != null)
                {
                    response.Status = validationResult.Status;
                    response.Details = validationResult.Details;
                    return response;
                }

                var pending = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Pending);

                var analyst = await _analystRepository.GetAnalystWithLeastWorkloadAsync(pending.Id);

                if (analyst == null)
                    throw new Exception("No hay analistas activos disponibles.");

                Domain.Entities.Authorization authorizationToAdd = new()
                {
                    AffiliateId = validationResult.Affiliate.Id,
                    ApplicationAmount = command.ApplicationAmount,
                    ApplicationDate = DateOnly.FromDateTime(DateTime.Now),
                    AssignedAnalyst = analyst.Id,
                    AuthorizationTypeId = type.Id,
                    HospitalId = hospital.Id,
                    HospitalApplicationId = command.HospitalApplicationId,
                    PolicyId = validationResult.Policy.Id
                };

                var planCoverages = await _planCoverageRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.PlanCoverage, object>>>
                {
                    m => m.Plan,
                    m => m.Coverage
                });

                var policyCoverages = planCoverages.Where(p => p.PlanId == validationResult.Policy.PlanId).ToList();

                if(!policyCoverages.Any(p => p.Coverage.Name == type.Name))
                {
                    var rejected = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Rejected);
                    authorizationToAdd.StatusId = rejected.Id;
                    authorizationToAdd.ApprovedAmount = 0;
                    response.AuthorizationStatus = "Rechazada";
                }
                else
                {
                    var coverage = policyCoverages.FirstOrDefault(p => p.Coverage.Name == type.Name);

                    if (coverage.AmountLimit > command.ApplicationAmount && coverage.YearFrequencyLimit == 0 && coverage.CoveragePercentage > 0)
                    {
                        var approved = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Approved);
                        authorizationToAdd.StatusId = approved.Id;
                        authorizationToAdd.ApprovedAmount = command.ApplicationAmount * (coverage.CoveragePercentage / 100);
                        response.AuthorizationStatus = "Aprobada";
                    }
                    else
                    {
                        authorizationToAdd.StatusId = pending.Id;
                        response.AuthorizationStatus = "Pendiente";
                    }
                }

                var authorization = await _authorizationRepository.AddAsync(authorizationToAdd);

                try
                {
                    NotificationDTO dto = new()
                    {
                        AuthorizationId = authorization.Id,
                        Message = "¡Te han asignado una nueva autorización!",
                        NotificationDate = DateTime.UtcNow,
                        AssignedAnalyst = authorization.AssignedAnalyst
                    };

                    var analystUser = await _accountService.GetUsersById(authorization.AssignedAnalyst);

                    await _notificationSender.SendAuthorizationAssignedAsync(analystUser.UserName, dto);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Hubo un error mientras se intentó notificar al analista");
                }

                response.Id = authorization.Id;
                response.DocumentType = command.DocumentType;
                response.DocumentNumber = command.DocumentNumber;
                response.PolicyNumber = validationResult.Policy.Number;
                response.AuthorizationType = type.Name;
                response.ApplicationAmount = authorization.ApplicationAmount;
                response.ApprovedAmount = authorization.ApprovedAmount;
                response.ApplicationDate = authorization.ApplicationDate;
                response.Hospital = hospital.Name;
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO() { Code = "000", Message = "Se registró la solicitud correctamente" }];
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
