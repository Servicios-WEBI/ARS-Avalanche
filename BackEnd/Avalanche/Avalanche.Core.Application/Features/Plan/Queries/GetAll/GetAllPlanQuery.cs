using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.Plan.Queries.GetAll
{
    public class GetAllPlanQuery : IRequest<GetAllPlanQueryResponse>
    {

    }

    public class GetAllPlanQueryHandler : IRequestHandler<GetAllPlanQuery, GetAllPlanQueryResponse>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public GetAllPlanQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<GetAllPlanQueryResponse> Handle(GetAllPlanQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllPlanQueryResponse result = new();

                var getAlls = await _planRepository.GetAllAsync();
                var plans = _mapper.Map<List<GetAllPlanQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.Plans = plans;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
