using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Authorization.Queries.GetById
{
    public class GetByIdAuthorizationQuery : IRequest<GetByIdAuthorizationQueryResponse>
    {
        public string Id { get; set; }
    }

    public class GetByIdAuthorizationQueryHandler : IRequestHandler<GetByIdAuthorizationQuery, GetByIdAuthorizationQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IPlanRepository _planRepository;

        public GetByIdAuthorizationQueryHandler(IAuthorizationRepository authorizationRepository, IDocumentTypeRepository documentTypeRepository,
            IPlanRepository planRepository)
        {
            _authorizationRepository = authorizationRepository;
            _documentTypeRepository = documentTypeRepository;
            _planRepository = planRepository;
        }

        public async Task<GetByIdAuthorizationQueryResponse> Handle(GetByIdAuthorizationQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetByIdAuthorizationQueryResponse result = new();

                var entity = await _authorizationRepository.GetByIdWithIncludeAsync(a => a.Id == query.Id, new List<Expression<Func<Domain.Entities.Authorization, object>>>
                {
                    m => m.Affiliate,
                    m => m.Analyst,
                    m => m.AuthorizationType,
                    m => m.Hospital,
                    m => m.Policy,
                    m => m.Status
                });

                if (entity == null)
                {
                    throw new Exception(ErrorMessages.NotFound);
                }

                var document = await _documentTypeRepository.GetByIdAsync(entity.Affiliate.DocumentTypeId);
                var plan = await _planRepository.GetByIdAsync(entity.Policy.PlanId);

                result.Authorization = new GetByIdAuthorizationQueryResponseChild()
                {
                    Id = entity.Id,
                    ApplicationDate = entity.ApplicationDate,
                    ApplicationAmount = entity.ApplicationAmount,
                    ApprovedAmount = entity.ApprovedAmount,
                    Affiliate = entity.Affiliate.FirstName + " " + entity.Affiliate.LastName,
                    AffiliateId = entity.AffiliateId,
                    Status = entity.Status.Name,
                    StatusId = entity.StatusId,
                    AuthorizationType = entity.AuthorizationType.Name,
                    AuthorizationTypeId = entity.AuthorizationTypeId,
                    DocumentType = document.Name,
                    DocumentNumber = entity.Affiliate.DocumentNumber,
                    Hospital = entity.Hospital.Name,
                    HospitalId = entity.HospitalId,
                    PolicyNumber = entity.Policy.Number,
                    PolicyId = entity.PolicyId,
                    AssignedAnalyst = entity.Analyst.FullName,
                    Plan = plan.Name
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
