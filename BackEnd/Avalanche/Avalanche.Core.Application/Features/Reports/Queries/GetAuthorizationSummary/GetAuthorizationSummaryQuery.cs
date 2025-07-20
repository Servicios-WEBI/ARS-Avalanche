using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Reports.Queries.GetAuthorizationSummary
{
    public class GetAuthorizationSummaryQuery : IRequest<GetAuthorizationSummaryQueryResponse>
    {
        [SwaggerParameter(Description = "Mes")]
        [Range(1, 12, ErrorMessage = "Debe ingresar un mes válido")]
        public int Month { get; set; }

        [SwaggerParameter(Description = "Año")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar el año")]
        public int Year { get; set; }
    }

    public class GetAuthorizationSummaryQueryHandler : IRequestHandler<GetAuthorizationSummaryQuery, GetAuthorizationSummaryQueryResponse>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public GetAuthorizationSummaryQueryHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<GetAuthorizationSummaryQueryResponse> Handle(GetAuthorizationSummaryQuery query, CancellationToken cancellationToken)
        {
            try
            {
                GetAuthorizationSummaryQueryResponse result = new();

                try
                {
                    var (start, end) = DateRangeHelper.GetDateRange(query.Year, query.Month);
                    var (prevStart, prevEnd) = DateRangeHelper.GetPreviousMonthRange(query.Year, query.Month);

                    result = await _authorizationRepository.GetAuthorizationSummaryAsync(start, end, prevStart, prevEnd);
                    
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
