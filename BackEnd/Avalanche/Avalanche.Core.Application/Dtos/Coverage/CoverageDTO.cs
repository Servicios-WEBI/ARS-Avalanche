using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Coverage
{
    public class CoverageDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
