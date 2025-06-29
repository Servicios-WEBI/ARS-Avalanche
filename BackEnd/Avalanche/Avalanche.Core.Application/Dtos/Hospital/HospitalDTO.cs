using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Hospital
{
    public class HospitalDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string InstitutionTypeId { get; set; }
        public string StatusId { get; set; }
    }
}
