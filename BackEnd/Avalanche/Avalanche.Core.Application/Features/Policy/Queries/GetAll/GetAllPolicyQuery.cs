using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Policy.Queries.GetAll
{
    public class GetAllPolicyQuery : IRequest<GetAllPolicyQueryResponse>
    {

    }

    public class GetAllPolicyQueryHandler : IRequestHandler<GetAllPolicyQuery, GetAllPolicyQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;

        public GetAllPolicyQueryHandler(IPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<GetAllPolicyQueryResponse> Handle(GetAllPolicyQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllPolicyQueryResponse result = new();

                var getAlls = await _policyRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Policy, object>>>
                {
                    m => m.Client,
                    m => m.Plan,
                    m => m.Status
                });
                var entities = getAlls.OrderByDescending(x => x.Created).ToList();

                var policies = entities.Select(p => new GetAllPolicyQueryResponseChild
                {
                    Id = p.Id,
                    Number = p.Number,
                    EffectiveStartDate = p.EffectiveStartDate,
                    EffectiveEndDate = p.EffectiveEndDate,
                    Client = p.Client.FirstName + " " + p.Client.LastName,
                    Plan = p.Plan.Name,
                    Status = p.Status.Name
                }).ToList();

                result.Policies = policies;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
