using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Client.Command.Update
{
    public class UpdateClientCommand : IRequest<ClientDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Primer nombre")]
        [Required(ErrorMessage = "Debe ingresar el primer nombre")]
        public string FirstName { get; set; }

        [SwaggerParameter(Description = "Segundo nombre")]
        public string? MiddleName { get; set; }

        [SwaggerParameter(Description = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string LastName { get; set; }

        [SwaggerParameter(Description = "Tipo de documento")]
        [Required(ErrorMessage = "Debe ingresar el tipo de documento")]
        public string DocumentType { get; set; }

        [SwaggerParameter(Description = "Número de documento")]
        [Required(ErrorMessage = "Debe ingresar el número de documento")]
        public string DocumentNumber { get; set; }

        [SwaggerParameter(Description = "Número de telefono")]
        [Required(ErrorMessage = "Debe ingresar el número de telefono")]
        public string Phone { get; set; }

        [SwaggerParameter(Description = "Correo electrónico")]
        [Required(ErrorMessage = "Debe ingresar el correo electrónico")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "Dirección")]
        public string? Address { get; set; }

        [SwaggerParameter(Description = "Estado")]
        [Required(ErrorMessage = "Debe ingresar el nuevo estado")]
        public string Status { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDTO>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAffiliateRepository _affiliateRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IAffiliateRepository affiliateRepository,
            IDocumentTypeRepository documentTypeRepository, IStatusRepository statusRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _affiliateRepository = affiliateRepository;
            _documentTypeRepository = documentTypeRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            try
            {
                ClientDTO response = new();

                var valueToUpdate = await _clientRepository.GetByIdAsync(command.Id);

                if (valueToUpdate == null)
                    throw new Exception(ErrorMessages.NotFound);

                var documentType = await _documentTypeRepository.GetByPropertyAsync(dt => dt.Name == command.DocumentType.ToUpper());
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el tipo de documento: {command.DocumentType.ToUpper()}");
                }
                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == command.Status);
                if (status == null)
                {
                    throw new Exception($"No se encontró el estado: {command.Status}");
                }

                valueToUpdate.FirstName = command.FirstName;
                valueToUpdate.MiddleName = command.MiddleName;
                valueToUpdate.LastName = command.LastName;
                valueToUpdate.DocumentTypeId = documentType.Id;
                valueToUpdate.DocumentNumber = command.DocumentNumber;
                valueToUpdate.Phone = command.Phone;
                valueToUpdate.Email = command.Email;
                valueToUpdate.Address = command.Address;
                valueToUpdate.StatusId = status.Id;

                await _clientRepository.UpdateAsync(valueToUpdate, valueToUpdate.Id);

                try
                {
                    var affiliate = await _affiliateRepository.GetByIdAsync(valueToUpdate.Id);

                    affiliate.FirstName = command.FirstName;
                    affiliate.MiddleName = command.MiddleName;
                    affiliate.LastName = command.LastName;
                    affiliate.DocumentTypeId = documentType.Id;
                    affiliate.DocumentNumber = command.DocumentNumber;
                    affiliate.StatusId = status.Id;

                    await _affiliateRepository.UpdateAsync(affiliate, affiliate.Id);
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error al actualizar datos del afiliado");
                }

                response = _mapper.Map<ClientDTO>(valueToUpdate);
                response.DocumentType = command.DocumentType;
                response.ClientStatus = command.Status;
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se modificó correctamente el cliente" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
