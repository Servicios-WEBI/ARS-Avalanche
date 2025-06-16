using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.Coverage.Queries.GetAll
{
    public class GetAllCoverageQuery : IRequest<GetAllCoverageQueryResponse>
    {

    }

    public class GetAllCoverageQueryHandler : IRequestHandler<GetAllCoverageQuery, GetAllCoverageQueryResponse>
    {
        private readonly ICoverageRepository _coverageRepository;
        private readonly IMapper _mapper;

        public GetAllCoverageQueryHandler(ICoverageRepository coverageRepository, IMapper mapper)
        {
            _coverageRepository = coverageRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCoverageQueryResponse> Handle(GetAllCoverageQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllCoverageQueryResponse result = new();

                var getAlls = await _coverageRepository.GetAllAsync();
                var coverages = _mapper.Map<List<GetAllCoverageQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.Coverages = coverages;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
