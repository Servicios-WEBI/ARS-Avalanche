using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Queries.ValidateAffiliate
{
    public class ValidateAffiliateQuery : IRequest<ValidateAffiliateQueryResponse>
    {
        [SwaggerParameter(Description = "Tipo de documento")]
        public string? DocumentType { get; set; }

        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe de ingresar el número de documento")]
        public string DocumentNumber { get; set; }

        [SwaggerParameter(Description = "Número de poliza")]
        public string? PolicyNumber { get; set; }
    }

    public class ValidateAffiliateQueryHandler : IRequestHandler<ValidateAffiliateQuery, ValidateAffiliateQueryResponse>
    {
        private readonly IAffiliateValidationService _validationService;
        private readonly ICoverageRepository _coverageRepository;
        private readonly IPlanRepository _planRepository;

        public ValidateAffiliateQueryHandler(IAffiliateValidationService validationService, ICoverageRepository coverageRepository,
            IPlanRepository planRepository)
        {
            _validationService = validationService;
            _coverageRepository = coverageRepository;
            _planRepository = planRepository;
        }

        public async Task<ValidateAffiliateQueryResponse> Handle(ValidateAffiliateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                ValidateAffiliateQueryResponse result = new();

                var validationResult = await _validationService.ValidateAsync(query.DocumentType, query.DocumentNumber, query.PolicyNumber);

                if (validationResult.Status != null)
                {
                    result.Exists = false;
                    result.Status = validationResult.Status;
                    result.Details = validationResult.Details;
                    return result;
                }

                var plan = await _planRepository.GetByIdWithIncludeAsync(t => t.Id == validationResult.Policy.PlanId, new List<Expression<Func<Domain.Entities.Plan, object>>>
                {
                    m => m.PlanCoverages
                });

                List<PlanCoverageResponseDTO> coverages = new();

                foreach (var item in plan.PlanCoverages)
                {
                    var coverage = await _coverageRepository.GetByIdAsync(item.CoverageId);

                    PlanCoverageResponseDTO response = new()
                    {
                        Name = coverage.Name,
                        Description = coverage.Description,
                        AmountLimit = item.AmountLimit,
                        YearFrequencyLimit = item.YearFrequencyLimit,
                        CoveragePercentage = item.CoveragePercentage
                    };

                    coverages.Add(response);
                }

                result.Exists = true;
                result.Name = validationResult.Affiliate.FirstName + " " + validationResult.Affiliate.LastName;
                result.AffiliateDate = validationResult.Affiliate.AffiliateDate;
                result.Number = validationResult.Policy.Number;
                result.PolicyStatus = validationResult.Policy.Status.Name;
                result.Plan = plan.Name;
                result.Coverages = coverages;
                result.Status = "El afiliado está disponible";
                result.Details = [new ErrorDetailsDTO() { Code = "000", Message = "El afiliado se encuentra disponible" }];
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
