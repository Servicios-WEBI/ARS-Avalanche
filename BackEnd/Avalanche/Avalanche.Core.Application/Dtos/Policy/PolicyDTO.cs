using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Policy
{
    public class PolicyDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public DateOnly EffectiveStartDate { get; set; }
        public DateOnly? EffectiveEndDate { get; set; }
        public string PolicyStatus { get; set; }
        public string ClientId { get; set; }
        public string PlanId { get; set; }
    }
}
