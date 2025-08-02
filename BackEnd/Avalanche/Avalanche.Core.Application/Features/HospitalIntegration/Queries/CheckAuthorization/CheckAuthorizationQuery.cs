using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Queries.CheckAuthorization
{
    public class CheckAuthorizationQuery : IRequest<CheckAuthorizationQueryResponse>
    {
        [SwaggerParameter(Description = "Número de solicitud")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe de ingresar el número de solicitud")]
        public int AuthorizationNumber { get; set; }
    }

    public class CheckAuthorizationQueryHandler : IRequestHandler<CheckAuthorizationQuery, CheckAuthorizationQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public CheckAuthorizationQueryHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<CheckAuthorizationQueryResponse> Handle(CheckAuthorizationQuery query, CancellationToken cancellationToken)
        {
            try
            {
                CheckAuthorizationQueryResponse result = new();

                var entity = await _authorizationRepository.GetByPropertyWithIncludeAsync(t => t.HospitalApplicationId == query.AuthorizationNumber, new List<Expression<Func<Domain.Entities.Authorization, object>>>
                {
                    m => m.Affiliate,
                    m => m.AuthorizationType,
                    m => m.Hospital,
                    m => m.Policy,
                    m => m.Status
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                result.Authorization = new()
                {
                    Number = entity.HospitalApplicationId,
                    ApplicationDate = entity.ApplicationDate,
                    Status = entity.Status.Name,
                    AuthorizationType = entity.AuthorizationType.Name,
                    ApplicationAmount = entity.ApplicationAmount,
                    ApprovedAmount = entity.ApprovedAmount,
                    Affiliate = entity.Affiliate.FirstName + " " + entity.Affiliate.LastName,
                    Policy = entity.Policy.Number,
                    Hospital = entity.Hospital.Name
                };

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
