using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.Status.Queries.GetAll
{
    public class GetAllStatusQuery : IRequest<GetAllStatusQueryResponse>
    {

    }

    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, GetAllStatusQueryResponse>
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public GetAllStatusQueryHandler(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<GetAllStatusQueryResponse> Handle(GetAllStatusQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllStatusQueryResponse result = new();

                var getAlls = await _statusRepository.GetAllAsync();
                var status = _mapper.Map<List<GetAllStatusQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.Statuses = status;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
