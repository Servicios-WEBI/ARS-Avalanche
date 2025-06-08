using Avalanche.Core.Domain.Common;

namespace Avalanche.Core.Domain.Entities
{
    public class Client : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateOnly CustomerSince { get; set; }
        public string Status { get; set; }
        public List<Affiliate> Affiliates { get; set; }
        public List<Policy> Policies { get; set; }

        public Client() {
            this.Id = "";
        }
    }
}
