using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Authorization.Queries.GetAll
{
    public class GetAllAuthorizationQuery : IRequest<GetAllAuthorizationQueryResponse>
    {
        public string? AssignedAnalyst {  get; set; }
        public string? Status { get; set; }
    }

    public class GetAllAuthorizationQueryHandler : IRequestHandler<GetAllAuthorizationQuery, GetAllAuthorizationQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IStatusRepository _statusRepository;

        public GetAllAuthorizationQueryHandler(IAuthorizationRepository authorizationRepository, IStatusRepository statusRepository)
        {
            _authorizationRepository = authorizationRepository;
            _statusRepository = statusRepository;
        }

        public async Task<GetAllAuthorizationQueryResponse> Handle(GetAllAuthorizationQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllAuthorizationQueryResponse result = new();
                List<Domain.Entities.Authorization> getAlls = new();

                if (!string.IsNullOrWhiteSpace(query.AssignedAnalyst))
                {
                    getAlls = await _authorizationRepository.GetAllByPropertyWithIncludeAsync(a => a.AssignedAnalyst == query.AssignedAnalyst,
                        new List<Expression<Func<Domain.Entities.Authorization, object>>>
                    {
                        m => m.Affiliate,
                        m => m.Analyst,
                        m => m.AuthorizationType,
                        m => m.Hospital,
                        m => m.Policy,
                        m => m.Status
                    });
                }
                else
                {
                    getAlls = await _authorizationRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Authorization, object>>>
                    {
                        m => m.Affiliate,
                        m => m.Analyst,
                        m => m.AuthorizationType,
                        m => m.Hospital,
                        m => m.Policy,
                        m => m.Status
                    });
                }

                if (!string.IsNullOrWhiteSpace(query.Status))
                {
                    var status = await _statusRepository.GetByPropertyAsync(s => s.Name == query.Status);
                    if (status == null)
                        throw new Exception("Ese estado no existe en el sistema");

                    getAlls = getAlls.Where(a => a.StatusId == status.Id).ToList();
                }

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
                    AssignedAnalyst = a.Analyst.FullName,
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
