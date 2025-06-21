using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Client.Command.Delete
{
    public class DeleteClientCommand : IRequest<ClientDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, ClientDTO>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
        {
            try
            {
                ClientDTO response = new();
                var valueToDelete = await _clientRepository.GetByIdWithIncludeAsync(e => e.Id == command.Id, new List<Expression<Func<Domain.Entities.Client, object>>>
                {
                    m => m.DocumentType
                });

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _clientRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<ClientDTO>(valueToDelete);
                response.DocumentType = valueToDelete.DocumentType.Name;
                response.ClientStatus = "Eliminado";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente el cliente" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
