using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Hospital.Queries.GetAll
{
    public class GetAllHospitalQuery : IRequest<GetAllHospitalQueryResponse>
    {

    }

    public class GetAllHospitalQueryHandler : IRequestHandler<GetAllHospitalQuery, GetAllHospitalQueryResponse>
    {
        private readonly IHospitalRepository _hospitalRepository;

        public GetAllHospitalQueryHandler(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        public async Task<GetAllHospitalQueryResponse> Handle(GetAllHospitalQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAllHospitalQueryResponse result = new();

                var getAlls = await _hospitalRepository.GetAllWithIncludeAsync(new List<Expression<Func<Domain.Entities.Hospital, object>>>
                {
                    m => m.InstitutionType,
                    m => m.Status
                });

                var hospitals = getAlls.OrderByDescending(x => x.Created).ToList();

                result.Hospitals = hospitals.Select(h => new GetAllHospitalQueryResponseChild()
                {
                    Id = h.Id,
                    Name = h.Name,
                    InstitutionType = h.InstitutionType.Name,
                    InstitutionTypeId = h.InstitutionTypeId,
                    Status = h.Status.Name,
                    StatusId = h.StatusId
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
