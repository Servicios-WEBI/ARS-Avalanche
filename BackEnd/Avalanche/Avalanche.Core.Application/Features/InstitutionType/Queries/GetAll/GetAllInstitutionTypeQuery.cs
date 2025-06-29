using AutoMapper;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;

namespace Avalanche.Core.Application.Features.InstitutionType.Queries.GetAll
{
    public class GetAllInstitutionTypeQuery : IRequest<GetAllInstitutionTypeQueryResponse>
    {

    }

    public class GetAllInstitutionTypeQueryHandler : IRequestHandler<GetAllInstitutionTypeQuery, GetAllInstitutionTypeQueryResponse>
    {
        private readonly IInstitutionTypeRepository _institutionTypeRepository;
        private readonly IMapper _mapper;

        public GetAllInstitutionTypeQueryHandler(IInstitutionTypeRepository institutionTypeRepository, IMapper mapper)
        {
            _institutionTypeRepository = institutionTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetAllInstitutionTypeQueryResponse> Handle(GetAllInstitutionTypeQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllInstitutionTypeQueryResponse result = new();

                var getAlls = await _institutionTypeRepository.GetAllAsync();
                var institutionTypes = _mapper.Map<List<GetAllInstitutionTypeQueryResponseChild>>(getAlls.OrderByDescending(x => x.Created).ToList());

                result.InstitutionTypes = institutionTypes;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
