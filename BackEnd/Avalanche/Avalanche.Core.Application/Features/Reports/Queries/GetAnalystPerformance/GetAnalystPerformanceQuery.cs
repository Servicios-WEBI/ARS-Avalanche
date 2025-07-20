using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAnalystPerformance
{
    public class GetAnalystPerformanceQuery : IRequest<GetAnalystPerformanceQueryResponse>
    {
        [SwaggerParameter(Description = "Mes")]
        [Range(1, 12, ErrorMessage = "Debe ingresar un mes válido")]
        public int Month { get; set; }

        [SwaggerParameter(Description = "Año")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar el año")]
        public int Year { get; set; }

        [SwaggerParameter(Description = "Top")]
        public int Top { get; set; }

        [SwaggerParameter(Description = "Analista")]
        public string? Analyst { get; set; }
    }

    public class GetAnalystPerformanceQueryHandler : IRequestHandler<GetAnalystPerformanceQuery, GetAnalystPerformanceQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public GetAnalystPerformanceQueryHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<GetAnalystPerformanceQueryResponse> Handle(GetAnalystPerformanceQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAnalystPerformanceQueryResponse result = new();

                try
                {
                    var (start, end) = DateRangeHelper.GetDateRange(query.Year, query.Month);

                    result = await _authorizationRepository.GetAnalystPerformanceAsync(start, end, query.Analyst, query.Top);
                    
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
