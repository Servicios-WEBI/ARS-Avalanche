using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Affiliate.Queries.GetAll
{
    public class GetAllAffiliateQuery : IRequest<GetAllAffiliateQueryResponse>
    {

    }

    public class GetAllAffiliateQueryHandler : IRequestHandler<GetAllAffiliateQuery, GetAllAffiliateQueryResponse>
    {
        private readonly IAffiliateRepository _affilliateRepository;

        public GetAllAffiliateQueryHandler(IAffiliateRepository affilliateRepository)
        {
            _affilliateRepository = affilliateRepository;
        }

        public async Task<GetAllAffiliateQueryResponse> Handle(GetAllAffiliateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllAffiliateQueryResponse result = new();

                var getAlls = await _affilliateRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.DocumentType,
                    m => m.Status,
                    m => m.Client,
                    m => m.AffiliatePolicies
                });
                var entities = getAlls.OrderByDescending(x => x.Created).ToList();

                var affilliates = entities.Select(a => new GetAllAffiliateQueryResponseChild
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    DocumentType = a.DocumentType.Name,
                    DocumentNumber = a.DocumentNumber,
                    BirthDate = a.BirthDate,
                    AffiliateDate = a.AffiliateDate,
                    Gender = a.Gender,
                    Status = a.Status.Name,
                    IsPrincipal = a.AffiliatePolicies.Count != 0 ? a.AffiliatePolicies[0].IsPrincipal : null,
                    ClientId = a.ClientId,
                    ClientName = a.Client.FirstName + " " + a.Client.LastName
                }).ToList();

                result.Affiliates = affilliates;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
