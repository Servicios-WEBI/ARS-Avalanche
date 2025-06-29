using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Authorization.Queries.GetAll
{
    public class GetAllAuthorizationQuery : IRequest<GetAllAuthorizationQueryResponse>
    {

    }

    public class GetAllAuthorizationQueryHandler : IRequestHandler<GetAllAuthorizationQuery, GetAllAuthorizationQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public GetAllAuthorizationQueryHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<GetAllAuthorizationQueryResponse> Handle(GetAllAuthorizationQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllAuthorizationQueryResponse result = new();

                var getAlls = await _authorizationRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Authorization, object>>>
                {
                    m => m.Affiliate,
                    m => m.AuthorizationType,
                    m => m.Hospital,
                    m => m.Policy,
                    m => m.Status
                });

                var authorizations = getAlls.OrderByDescending(x => x.Created).ToList();

                result.Authorizations = authorizations.Select(a => new GetAllAuthorizationQueryResponseChild()
                {
                    Id = a.Id,
                    ApplicationDate = a.ApplicationDate,
                    ApplicationAmount = a.ApplicationAmount,
                    ApprovedAmount = a.ApprovedAmount,
                    Affiliate = a.Affiliate.FirstName + " " + a.Affiliate.LastName,
                    AuthorizationType = a.AuthorizationType.Name,
                    Hospital = a.Hospital.Name,
                    Policy = a.Policy.Number,
                    Status = a.Status.Name
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
