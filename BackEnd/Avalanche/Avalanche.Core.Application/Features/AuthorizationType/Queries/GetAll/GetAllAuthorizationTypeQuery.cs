using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.AuthorizationType.Queries.GetAll
{
    public class GetAllAuthorizationTypeQuery : IRequest<GetAllAuthorizationTypeQueryResponse>
    {

    }

    public class GetAllAuthorizationTypeQueryHandler : IRequestHandler<GetAllAuthorizationTypeQuery, GetAllAuthorizationTypeQueryResponse>
    {
        private readonly IAuthorizationTypeRepository _authorizationTypeRepository;
        private readonly IMapper _mapper;

        public GetAllAuthorizationTypeQueryHandler(IAuthorizationTypeRepository authorizationTypeRepository, IMapper mapper)
        {
            _authorizationTypeRepository = authorizationTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetAllAuthorizationTypeQueryResponse> Handle(GetAllAuthorizationTypeQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllAuthorizationTypeQueryResponse result = new();

                var getAlls = await _authorizationTypeRepository.GetAllAsync();
                var authorizationTypes = _mapper.Map<List<GetAllAuthorizationTypeQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.AuthorizationTypes = authorizationTypes;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
