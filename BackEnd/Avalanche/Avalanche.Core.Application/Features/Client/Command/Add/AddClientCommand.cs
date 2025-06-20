using AutoMapper;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Client;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Avalanche.Core.Domain.Entities;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Client.Command.Add
{
    public class AddClientCommand : IRequest<ClientDTO>
    {
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

        [SwaggerParameter(Description = "Genero")]
        [Required(ErrorMessage = "Debe ingresar el genero")]
        public string Gender { get; set; }

        [SwaggerParameter(Description = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public DateOnly BirthDate { get; set; }

        [SwaggerParameter(Description = "Número de telefono")]
        [Required(ErrorMessage = "Debe ingresar el número de telefono")]
        public string Phone { get; set; }

        [SwaggerParameter(Description = "Correo electrónico")]
        [Required(ErrorMessage = "Debe ingresar el correo electrónico")]
        public string Email { get; set; }

        [SwaggerParameter(Description = "Dirección")]
        public string? Address { get; set; }  
    }

    public class AddClientCommandHandler : IRequestHandler<AddClientCommand, ClientDTO>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IAffiliateRepository _affiliateRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public AddClientCommandHandler(IClientRepository clientRepository, IDocumentTypeRepository documentTypeRepository,
            IAffiliateRepository affiliateRepository, IStatusRepository statusRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _documentTypeRepository = documentTypeRepository;
            _affiliateRepository = affiliateRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Handle(AddClientCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var documentType = await _documentTypeRepository.GetByNameAsync(command.DocumentType.ToUpper());
                if (documentType == null)
                {
                    throw new Exception($"No se encontró el tipo de documento: {command.DocumentType.ToUpper()}");
                }

                var client = await _clientRepository.GetByDocumentNumberAsync(a => a.DocumentNumber == command.DocumentNumber, new List<Expression<Func<Domain.Entities.Client, object>>> { });
                if (client != null)
                {
                    throw new Exception("Ya existe un cliente con ese número de documento");
                }

                var status = await _statusRepository.GetByNameAsync("Activo");

                ClientDTO response = new();
                var valueToAdd = _mapper.Map<Domain.Entities.Client>(command);
                valueToAdd.DocumentTypeId = documentType.Id;
                valueToAdd.StatusId = status.Id;
                valueToAdd.CustomerSince = DateOnly.FromDateTime(DateTime.UtcNow);

                var entity = await _clientRepository.AddAsync(valueToAdd);

                var affiliateToAdd = _mapper.Map<Domain.Entities.Affiliate>(entity);
                affiliateToAdd.AffiliateDate = DateOnly.FromDateTime(DateTime.UtcNow);
                affiliateToAdd.Gender = command.Gender;
                affiliateToAdd.BirthDate = command.BirthDate;
                affiliateToAdd.ClientId = entity.Id;

                try
                {
                    await _affiliateRepository.AddAsync(affiliateToAdd);
                }
                catch (Exception ex)
                {
                    await _clientRepository.DeleteAsync(entity);
                    throw new Exception("Hubo un error afiliando al cliente");
                }
                
                response = _mapper.Map<ClientDTO>(entity);
                response.DocumentType = command.DocumentType;
                response.ClientStatus = "Activo";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente el cliente" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
