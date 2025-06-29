using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Authorization;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Authorization.Command.Delete
{
    public class DeleteAuthorizationCommand : IRequest<AuthorizationDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeleteAuthorizationCommandHandler : IRequestHandler<DeleteAuthorizationCommand, AuthorizationDTO>
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IMapper _mapper;

        public DeleteAuthorizationCommandHandler(IAuthorizationRepository authorizationRepository, IMapper mapper)
        {
            _authorizationRepository = authorizationRepository;
            _mapper = mapper;
        }

        public async Task<AuthorizationDTO> Handle(DeleteAuthorizationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AuthorizationDTO response = new();
                var valueToDelete = await _authorizationRepository.GetByIdAsync(command.Id);

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _authorizationRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<AuthorizationDTO>(valueToDelete);
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente la autorización" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
