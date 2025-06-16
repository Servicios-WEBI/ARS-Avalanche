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
        private readonly IPlanRepository _cinemaRepository;
        private readonly IMapper _mapper;

        public GetAllPlanQueryHandler(IPlanRepository cinemaRepository, IMapper mapper)
        {
            _cinemaRepository = cinemaRepository;
            _mapper = mapper;
        }

        public async Task<GetAllPlanQueryResponse> Handle(GetAllPlanQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllPlanQueryResponse result = new();

                var getAlls = await _cinemaRepository.GetAllAsync();
                var cinemas = _mapper.Map<List<GetAllPlanQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.Plans = cinemas;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
