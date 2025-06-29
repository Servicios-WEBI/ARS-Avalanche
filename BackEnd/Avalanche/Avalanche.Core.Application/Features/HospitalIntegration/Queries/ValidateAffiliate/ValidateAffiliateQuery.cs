using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.PlanCoverage;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.HospitalIntegration.Queries.ValidateAffiliate
{
    public class ValidateAffiliateQuery : IRequest<ValidateAffiliateQueryResponse>
    {
        [SwaggerParameter(Description = "Tipo de documento")]
        [Required(ErrorMessage = "Debe de ingresar el tipo de documento")]
        public string DocumentType { get; set; }

        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe de ingresar el número de documento")]
        public string DocumentNumber { get; set; }

        [SwaggerParameter(Description = "Número de poliza")]
        public string? PolicyNumber { get; set; }
    }

    public class ValidateAffiliateQueryHandler : IRequestHandler<ValidateAffiliateQuery, ValidateAffiliateQueryResponse>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IAffiliateRepository _affiliateRepository;
        private readonly ICoverageRepository _coverageRepository;
        private readonly IPlanRepository _planRepository;

        public ValidateAffiliateQueryHandler(IPolicyRepository policyRepository, ICoverageRepository coverageRepository,
            IAffiliateRepository affiliateRepository, IPlanRepository planRepository)
        {
            _policyRepository = policyRepository;
            _affiliateRepository = affiliateRepository;
            _coverageRepository = coverageRepository;
            _planRepository = planRepository;
        }

        public async Task<ValidateAffiliateQueryResponse> Handle(ValidateAffiliateQuery query, CancellationToken cancellationToken)
        {
            try
            {
                ValidateAffiliateQueryResponse result = new();

                var entity = await _affiliateRepository.GetByDocumentNumberAsync(t => t.DocumentNumber == query.DocumentNumber, new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.AffiliatePolicies,
                    m => m.DocumentType,
                    m => m.Status
                });

                if (entity == null || entity.DocumentType.Name != query.DocumentType)
                {
                    result.Exists = false;
                    result.Status = "Afiliado no encontrado";
                    result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No existe un afiliado con ese tipo y número de documento" }];
                    return result;
                }

                if (entity.Status.Name != "Activo")
                {
                    result.Exists = false;
                    result.Status = "Afiliado no está activo";
                    result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "El afiliado debe contactar a la ARS" }];
                    return result;
                }

                if (entity.AffiliatePolicies.Count <= 0)
                {
                    result.Exists = false;
                    result.Status = "Afiliado no disponible";
                    result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "El afiliado debe contactar a la ARS" }];
                    return result;
                }

                var policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.AffiliatePolicies[0].PolicyId, new List<Expression<Func<Domain.Entities.Policy, object>>>
                {
                    m => m.Status
                });

                if (query.PolicyNumber != "" && query.PolicyNumber != null && policy.Number != query.PolicyNumber)
                {
                    result.Exists = false;
                    result.Status = "Afiliado no encontrado";
                    result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No existe esa poliza para ese afiliado" }];
                    return result;
                }

                var plan = await _planRepository.GetByIdWithIncludeAsync(t => t.Id == policy.PlanId, new List<Expression<Func<Domain.Entities.Plan, object>>>
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
                result.Name = entity.FirstName + " " + entity.LastName;
                result.AffiliateDate = entity.AffiliateDate;
                result.Number = policy.Number;
                result.PolicyStatus = policy.Status.Name;
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
