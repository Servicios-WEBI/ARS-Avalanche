using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationDistribution
{
    public class GetAuthorizationDistributionQuery : IRequest<GetAuthorizationDistributionQueryResponse>
    {
        [SwaggerParameter(Description = "Mes")]
        [Range(1, 12, ErrorMessage = "Debe ingresar un mes válido")]
        public int Month { get; set; }

        [SwaggerParameter(Description = "Año")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar el año")]
        public int Year { get; set; }
    }

    public class GetAuthorizationDistributionQueryHandler : IRequestHandler<GetAuthorizationDistributionQuery, GetAuthorizationDistributionQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public GetAuthorizationDistributionQueryHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<GetAuthorizationDistributionQueryResponse> Handle(GetAuthorizationDistributionQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAuthorizationDistributionQueryResponse result = new();

                try
                {
                    var (start, end) = DateRangeHelper.GetDateRange(query.Year, query.Month);
                    result = await _authorizationRepository.GetAuthorizationDistributionAsync(start, end);
                    
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
