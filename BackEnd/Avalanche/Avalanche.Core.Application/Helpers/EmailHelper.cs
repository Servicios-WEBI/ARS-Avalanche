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
                { "FullName", userWelcome.FullName },
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

        public static string MakeEmailForReset(string firstName, string lastName, string code)
        {
            string htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Código de Confirmación</title>
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
        .code {
            font-size: 32px;
            text-align: center;
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
            <h1 class='title'>Código de Confirmación</h1>
        </div>
        <div class='message'>
            <p>Hola [Nombre],</p>
            <p>Aquí tienes tu código de confirmación:</p>
        </div>
        <div class='code'>
            <p>[Código]</p>
        </div>
        <div class='footer'>
            <p>Por favor, ingresa este código para poder continuar con el proceso de restablecer la contraseña.</p>
			<p>Si no fuiste tú quien solicitó esta acción, revisa tu cuenta</p>
            <p>Atentamente,</p>
            <p>El equipo de Base</p>
        </div>
    </div>
</body>
</html>
";

            string html = htmlBody.Replace("[Nombre]", firstName + " " + lastName);
            html = html.Replace("[Código]", code);
            return html;
        }

        public static string MakeEmailForChange(string firstName, string lastName)
        {
            string htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Cambio de Contraseña</title>
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
            <h1 class='title'>Cambio de Contraseña</h1>
        </div>
        <div class='message'>
            <p>Hola [Nombre],</p>
            <p>Tu contraseña ha sido cambiada correctamente.</p>
            <p>Si no realizaste este cambio, por favor, ponte en contacto con nosotros lo antes posible.</p>
        </div>
        <div class='footer'>
            <p>Atentamente,</p>
            <p>El equipo de Base</p>
        </div>
    </div>
</body>
</html>
";

            string html = htmlBody.Replace("[Nombre]", firstName + " " + lastName);
            return html;
        }
    }
}
