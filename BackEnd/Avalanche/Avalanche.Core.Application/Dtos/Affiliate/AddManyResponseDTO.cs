using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Affiliate
{
    public class AddManyResponseDTO : ErrorDTO
    {
        public List<AddManyResponseChildDTO> Affiliates {get; set;}
    }

    public class AddManyResponseChildDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public DateOnly AffiliateDate { get; set; }
        public string AffiliateStatus { get; set; }
        public string ClientId { get; set; }
    }
}
