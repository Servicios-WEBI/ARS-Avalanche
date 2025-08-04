namespace Avalanche.Core.Application.Dtos.Email
{
    public class AuthorizationEmail
    {
        public string AnalystName { get; set; }
        public int AuthorizationId { get; set; }
        public string AuthorizationType { get; set; }
        public double ApplicationAmount { get; set; }
    }
}