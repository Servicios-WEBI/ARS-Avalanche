using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Application.Interfaces.Helpers;
using Avalanche.Core.Application.Interfaces.Services;

namespace Avalanche.Core.Application.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailTemplateService _service;

        public EmailHelper(IEmailTemplateService service)
        {
            _service = service;
        }

        public string MakeEmailForAnalyst(UserWelcomeEmail userWelcome)
        {
            return _service.RenderTemplate("AnalystWelcome", new Dictionary<string, string>
            {
                { "FullName", userWelcome.FullName },
                { "UserName", userWelcome.UserName },
                { "Password", userWelcome.Password }
            });
        }

        public string MakeEmailForAdmin(UserWelcomeEmail userWelcome)
        {
            return _service.RenderTemplate("AdminWelcome", new Dictionary<string, string>
            {
                { "FullName", userWelcome.FullName },
                { "UserName", userWelcome.UserName },
                { "Password", userWelcome.Password }
            });
        }

        public string MakeEmailForHospital(UserWelcomeEmail userWelcome)
        {
            return _service.RenderTemplate("HospitalWelcome", new Dictionary<string, string>
            {
                { "HospitalName", userWelcome.FullName },
                { "UserName", userWelcome.UserName },
                { "Password", userWelcome.Password }
            });
        }

        public static string MakeEmailForConfirmed(string user)
        {
            string htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Cuenta Confirmada</title>
    <style>
        /* Estilos adicionales */
        body {
            font-family: Arial, sans-serif;
        }
        .container {
            max-width: 400px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
            border-radius: 5px;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        .title {
            font-size: 24px;
            margin-bottom: 10px;
        }
        .message {
            font-size: 16px;
            margin-bottom: 20px;
        }
        .footer {
            text-align: center;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1 class='title'>¡Cuenta Confirmada!</h1>
        </div>
        <div class='message'>
            <p>Hola Nombre,</p>
            <p>Te damos la bienvenida a nuestra comunidad. Gracias por confirmar tu cuenta.</p>
            <p>Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos.</p>
            <p>¡Disfruta de la aplicación y que tengas un gran día!</p>
        </div>
        <div class='footer'>
            <p>Atentamente,</p>
            <p>El equipo de Base</p>
        </div>
    </div>
</body>
</html>
";
            string html = htmlBody.Replace("Nombre", user);
            return html;
        }

        public string MakeEmailForReset(string fullName, string code)
        {
            return _service.RenderTemplate("ConfirmationCode", new Dictionary<string, string>
            {
                { "FullName", fullName },
                { "ConfirmationCode", code }
            });
        }

        public string MakeEmailForChange(string fullName)
        {
            return _service.RenderTemplate("PasswordChanged", new Dictionary<string, string>
            {
                { "FullName", fullName },
            });
        }

        public string MakeEmailForNewAuthorization(AuthorizationEmail authorization)
        {
            return _service.RenderTemplate("NewAuthorization", new Dictionary<string, string>
            {
                { "AnalystName", authorization.AnalystName },
                { "AuthorizationId", authorization.AuthorizationId.ToString() },
                { "AuthorizationType", authorization.AuthorizationType },
                { "ApplicationAmount", authorization.ApplicationAmount.ToString() },
            });
        }
    }
}
