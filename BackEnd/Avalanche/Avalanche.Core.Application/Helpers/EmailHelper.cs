namespace Avalanche.Core.Application.Helpers
{
    public static class EmailHelper
    {
        public static string MakeEmailForConfirm(string verificationUri, string user)
        {
            string htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Confirmar Cuenta</title>
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
        .button {
            display: inline-block;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            user-select: none;
            border: 1px solid transparent;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: 0.25rem;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
            text-decoration: none;
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
            <h1 class='title'>Confirmar Cuenta</h1>
        </div>
        <div class='message'>
            <p>Hola [Nombre],</p>
            <p>Gracias por registrarte en nuestro sitio. Para completar el proceso de registro, por favor, haz clic en el siguiente botón:</p>
            <p><a href='[URL]' class='button'>Confirmar Cuenta</a></p>
        </div>
        <div class='footer'>
            <p>Atentamente,</p>
            <p>El equipo de Base</p>
        </div>
    </div>
</body>
</html>
";

            string html = htmlBody.Replace("[URL]", verificationUri);
            html = html.Replace("[Nombre]", user);
            return html;
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

        public static string MakeEmailForEmployee(List<string> requirements)
        {
            string htmlBody = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Registro Exitoso</title>
    <style>
        /* Estilos adicionales */
        body {
            font-family: Arial, sans-serif;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
            border-radius: 10px;
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
        .details {
            margin-bottom: 10px;
        }
        .details p {
            margin: 5px 0;
        }
        .password {
            font-weight: bold;
        }
        .warning {
            font-style: italic;
            color: red;
        }
        .button {
            display: inline-block;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            user-select: none;
            border: 1px solid transparent;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: 0.25rem;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
            text-decoration: none;
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
            <h1 class='title'>¡Registro Exitoso!</h1>
        </div>
        <div class='message'>
            <p>Estimado/a [Nombre],</p>
            <p>Te informamos que has sido registrado/a correctamente en [Empresa].</p>
            <div class='details'>
                <p><strong>Empresa:</strong> [Empresa]</p>
                <p><strong>Rol:</strong> [Rol]</p>
                <p><strong>Nombre de Usuario:</strong> [Usuario]</p>
                <p><strong>Contraseña por Defecto:</strong> <span class='password'>[Contraseña]</span></p>
            </div>
            <p class='warning'>Te recomendamos que cambies tu contraseña por defecto por motivos de seguridad.</p>
            <p>Puedes cambiarla desde la sección de olvidaste la contraseña de la página de inicio de sesión</p>
            <p>Ya puedes acceder al sistema y disfrutar de todas sus funcionalidades:</p>
            <p><a href='[URL]' class='button'>Ir al Sistema</a></p>
        </div>
        <div class='footer'>
            <p>Atentamente,</p>
            <p>El equipo de Base</p>
        </div>
    </div>
</body>
</html>
";

            string html = htmlBody.Replace("[Nombre]", requirements[0]);
            html = html.Replace("[Usuario]", requirements[1]);
            html = html.Replace("[Contraseña]", requirements[2]);
            html = html.Replace("[Empresa]", requirements[3]);
            html = html.Replace("[Rol]", requirements[4]);
            return html;
        }
    }
}
