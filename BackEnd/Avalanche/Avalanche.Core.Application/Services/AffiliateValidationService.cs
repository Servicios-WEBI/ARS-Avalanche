using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Services
{
    public class AffiliateValidationService : IAffiliateValidationService
    {

        private readonly IAffiliateRepository _affiliateRepository;
        private readonly IPolicyRepository _policyRepository;

        public AffiliateValidationService(
            IAffiliateRepository affiliateRepository,
            IPolicyRepository policyRepository)
        {
            _affiliateRepository = affiliateRepository;
            _policyRepository = policyRepository;
        }

        public async Task<AffiliateValidationResult> ValidateAsync(string documentType, string documentNumber,
            string? policyNumber,CancellationToken ct = default)
        {
            AffiliateValidationResult result = new();

            var entity = await _affiliateRepository.GetByPropertyWithIncludeAsync(t => t.DocumentNumber == documentNumber,
                new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.AffiliatePolicies,
                    m => m.DocumentType,
                    m => m.Status
                });

            if (entity == null || entity.DocumentType.Name != documentType)
            {
                result.Status = "Afiliado no encontrado";
                result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No existe un afiliado con ese tipo y número de documento" }];
                return result;
            }

            if (entity.Status.Name != "Activo")
            {
                result.Status = "Afiliado no está activo";
                result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "El afiliado debe contactar a la ARS" }];
                return result;
            }

            if (!entity.AffiliatePolicies.Any())
            {
                result.Status = "Afiliado no disponible";
                result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "El afiliado debe contactar a la ARS" }];
                return result;
            }

            var policy = await _policyRepository.GetByIdWithIncludeAsync(t => t.Id == entity.AffiliatePolicies[0].PolicyId, new List<Expression<Func<Domain.Entities.Policy, object>>>
            {
                m => m.Status
            });

            if (!string.IsNullOrWhiteSpace(policyNumber) && policy.Number != policyNumber)
            {
                result.Status = "Afiliado no encontrado";
                result.Details = [new ErrorDetailsDTO() { Code = ErrorMessages.NotFound, Message = "No existe esa poliza para ese afiliado" }];
                return result;
            }

            result.Affiliate = entity;
            result.Policy = policy;
            return result;
        }
    }
}
