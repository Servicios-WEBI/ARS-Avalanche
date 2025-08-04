using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Dtos.Email;
using Avalanche.Core.Application.Interfaces.Helpers;
using Avalanche.Core.Application.Interfaces.Services;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Account.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
	{
        [SwaggerParameter(Description = "Correo")]
        [Required(ErrorMessage = "Debe de ingresar su correo")]
        public string Email { get; set; }
    }

	public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
	{
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IEmailHelper _emailHelper;

        public ResetPasswordCommandHandler(IAccountService accountService, IEmailService emailService, IEmailHelper emailHelper)
        {
            _accountService = accountService;
            _emailService = emailService;
            _emailHelper = emailHelper;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
		{
			ResetPasswordResponse response = new();
			try
			{
				response = await _accountService.ResetPasswordAsync(command.Email);

                await _emailService.SendAsync(new EmailRequest()
                {
                    To = response.Email,
                    Body = _emailHelper.MakeEmailForReset(response.FullName, response.Code),
                    Subject = "¡Código de Confirmación ARS Avalanche!"
                });

                return response;
			}
			catch (Exception ex)
			{
				response.HasError = true;
				response.Error = "Ocurrió un error.";
				return response;
			}
		}

	}
}
