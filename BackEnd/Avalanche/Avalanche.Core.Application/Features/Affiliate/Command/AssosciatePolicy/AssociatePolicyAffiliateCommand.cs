using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Affiliate.Command.AssosciatePolicy
{
    public class AssociatePolicyAffiliateCommand : IRequest<AffiliatePolicyDTO>
    {
        [SwaggerParameter(Description = "Identificador del afiliado")]
        [Required(ErrorMessage = "Debe ingresar el afiliado")]
        public string AffiliateId { get; set; }

        [SwaggerParameter(Description = "Identificador de la póliza")]
        [Required(ErrorMessage = "Debe ingresar la póliza")]
        public string PolicyId { get; set; }

    }

    public class AssociatePolicyAffiliateCommandHandler : IRequestHandler<AssociatePolicyAffiliateCommand, AffiliatePolicyDTO>
    {
        private readonly IAffiliatePolicyRepository _affiliatePolicyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public AssociatePolicyAffiliateCommandHandler(IAffiliatePolicyRepository affiliatePolicyRepository, IStatusRepository statusRepository,
            IMapper mapper)
        {
            _affiliatePolicyRepository = affiliatePolicyRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<AffiliatePolicyDTO> Handle(AssociatePolicyAffiliateCommand command, CancellationToken cancellationToken)
        {
            try
            {

                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active, Properties.Name);

                AffiliatePolicyDTO response = new();
                var valueToAdd = _mapper.Map<AffiliatePolicy>(command);
                valueToAdd.IsPrincipal = false;
                valueToAdd.StatusId = status.Id;
                valueToAdd.AffiliationDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var entity = await _affiliatePolicyRepository.AddAsync(valueToAdd);

                response = _mapper.Map<AffiliatePolicyDTO>(entity);
                response.AffiliateStatus = "Activo";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "El afiliado está bajo la poliza" }];
                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
