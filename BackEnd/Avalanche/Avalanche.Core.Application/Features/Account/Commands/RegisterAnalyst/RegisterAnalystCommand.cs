using AutoMapper;
using Avalanche.Core.Application.Dtos.Account;
using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Account.Commands.RegisterAnalyst
{
    public class RegisterAnalystCommand : IRequest<RegisterResponse>
    {
        [SwaggerParameter(Description = "Nombre")]
        [Required(ErrorMessage = "Debe de ingresar el nombre")]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "Apellido")]
        [Required(ErrorMessage = "Debe de ingresar el apellido")]
        public string LastName { get; set; }

        [SwaggerParameter(Description = "Teléfono")]
        [Required(ErrorMessage = "Debe de ingresar el telefono")]
        public string PhoneNumber { get; set; }

        [SwaggerParameter(Description = "Correo")]
        [Required(ErrorMessage = "Debe de ingresar el correo")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "Dirección")]
        [Required(ErrorMessage = "Debe de ingresar el dirección")]
        public string Address { get; set; }

        [SwaggerParameter(Description = "Foto de perfil")]
		public IFormFile? Image { get; set; }

        [SwaggerParameter(Description = "Nombre de usuario")]
        [Required(ErrorMessage = "Debe de ingresar el nombre de usuario")]
        public string UserName { get; set; }
    }

    public class RegisterAnalystCommandHandler : IRequestHandler<RegisterAnalystCommand, RegisterResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public RegisterAnalystCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }


        public async Task<RegisterResponse> Handle(RegisterAnalystCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<RegisterRequest>(command);
                if(command.Image != null)
                {
                    request.UrlImage = ImageUpload.UploadImageUser(command.Image);
                }
                else
                {
                    request.UrlImage = "";
                }
                request.Password = "ARS@" + Guid.NewGuid().ToString().Substring(0,8);
                
                var response = await _accountService.RegisterAnalystAsync(request);

                if (response.Status == "Fallido")
				{
                    ImageUpload.DeleteFile(request.UrlImage);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
