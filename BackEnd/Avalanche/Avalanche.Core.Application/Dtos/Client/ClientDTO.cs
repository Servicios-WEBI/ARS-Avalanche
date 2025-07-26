using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Dtos.Client
{
    public class ClientDTO : ErrorDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateOnly CustomerSince { get; set; }
        public string ClientStatus { get; set; }
    }
}
