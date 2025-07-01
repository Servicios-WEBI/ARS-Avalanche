using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.HospitalIntegration;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Command.PayBills
{
    public class PayBillsCommand : IRequest<PayBillResponseDTO>
    {
        [SwaggerParameter(Description = "Listado de solicitudes a pagar")]
        [Required(ErrorMessage = "Debe de ingresar el listado de solicitudes")]
        public List<PayBillRequestDTO> Bills { get; set; }

        [SwaggerParameter(Description = "Hospital")]
        [Required(ErrorMessage = "Debe de ingresar el hospital")]
        public string Hospital { get; set; }
    }

    public class PayBillsCommandHandler : IRequestHandler<PayBillsCommand, PayBillResponseDTO>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IHospitalRepository _hospitalRepository;

        public PayBillsCommandHandler(IAuthorizationRepository authorizationRepository, IHospitalRepository hospitalRepository)
        {
            _authorizationRepository = authorizationRepository;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<PayBillResponseDTO> Handle(PayBillsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PayBillResponseDTO response = new();
                response.Bills = new();
                var hospital = await _hospitalRepository.GetByNameAsync(command.Hospital.ToUpper());

                if(hospital == null)
                {
                    response.Status = "Fallido";
                    response.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No se encontó ese hospital" }];
                    return response;
                }

                foreach (var item in command.Bills)
                {
                    BillDTO bill = new();
                    var authorization = await _authorizationRepository.GetByIdWithIncludeAsync(t => t.Id == item.AuthorizationNumber, new List<Expression<Func<Domain.Entities.Authorization, object>>>
                    {
                        m => m.Hospital,
                        m => m.Status
                    });

                    if (authorization == null)
                    {
                        bill.AuthorizationNumber = item.AuthorizationNumber;
                        bill.Status = "Rechazada";
                        bill.Details = "La solicitud no fue encontrada en nuestro sistema";
                        response.Bills.Add(bill);
                        response.TotalAmount += item.Amount;
                        response.RefusedAmount += item.Amount;
                        continue;
                    }
                    else if (authorization.Status.Name != "Aprobado")
                    {
                        bill.AuthorizationNumber = item.AuthorizationNumber;
                        bill.Status = "Rechazada";
                        bill.Details = "La solicitud no ha sido aprobada";
                        response.Bills.Add(bill);
                        response.TotalAmount += item.Amount;
                        response.RefusedAmount += item.Amount;
                        continue;
                    }
                    else if(authorization.Hospital.Name.ToUpper() != command.Hospital.ToUpper())
                    {
                        bill.AuthorizationNumber = item.AuthorizationNumber;
                        bill.Status = "Rechazada";
                        bill.Details = "La solicitud no fue realizada por el hospital";
                        response.Bills.Add(bill);
                        response.TotalAmount += item.Amount;
                        response.RefusedAmount += item.Amount;
                        continue;
                    }
                    else if (authorization.ApprovedAmount != item.Amount)
                    {
                        bill.AuthorizationNumber = item.AuthorizationNumber;
                        bill.Status = "Pagada parcialmente";
                        bill.Details = $"El monto aprobado fue de {(authorization.ApprovedAmount == null ? 0 : authorization.ApprovedAmount)}";
                        response.Bills.Add(bill);
                        response.TotalAmount += item.Amount;
                        response.RefusedAmount += item.Amount;
                        response.PaidAmount += authorization.ApprovedAmount == null ? 0 : (double)authorization.ApprovedAmount;
                        continue;
                    }

                    response.TotalAmount += item.Amount;
                    response.PaidAmount += item.Amount;
                    bill.AuthorizationNumber = item.AuthorizationNumber;
                    bill.Status = "Pagada";
                    bill.Details = "La solicitud fue pagada satisfactoriamente";
                    response.Bills.Add(bill);
                }

                response.TransferenceId = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se procesaron correctamente las facturas" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
