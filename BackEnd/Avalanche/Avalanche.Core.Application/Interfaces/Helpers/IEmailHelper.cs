using Avalanche.Core.Application.Dtos.Email;

namespace Avalanche.Core.Application.Interfaces.Helpers
{
    public interface IEmailHelper
    {
        string MakeEmailForAnalyst(UserWelcomeEmail userWelcome);
        string MakeEmailForAdmin(UserWelcomeEmail userWelcome);
        string MakeEmailForHospital(UserWelcomeEmail userWelcome);
        string MakeEmailForReset(string fullName, string code);
        string MakeEmailForChange(string fullName);
        string MakeEmailForNewAuthorization(AuthorizationEmail authorization);
    }
}