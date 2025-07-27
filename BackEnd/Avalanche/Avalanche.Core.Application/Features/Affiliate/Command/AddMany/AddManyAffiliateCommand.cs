using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Affiliate.Command.AddMany
{
    public class AddManyAffiliateCommand : IRequest<AddManyResponseDTO>
    {
        [SwaggerParameter(Description = "Lista de afiliados")]
        [Required(ErrorMessage = "Debe ingresar los afiliados")]
        public List<AffiliateRequestDTO> Affiliates { get; set; }
    }

    public class AddManyAffiliateCommandHandler : IRequestHandler<AddManyAffiliateCommand, AddManyResponseDTO>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public AddManyAffiliateCommandHandler(IAffiliateRepository affilliateRepository, IClientRepository clientRepository, 
            IDocumentTypeRepository documentTypeRepository, IStatusRepository statusRepository,
            IMapper mapper)
        {
            _affilliateRepository = affilliateRepository;
            _clientRepository = clientRepository;
            _documentTypeRepository = documentTypeRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<AddManyResponseDTO> Handle(AddManyAffiliateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AddManyResponseDTO response = new();
                response.Details = new();
                List<Domain.Entities.Affiliate> finalList = new();

                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active);

                foreach (var item in command.Affiliates)
                {
                    try
                    {
                        ErrorDetailsDTO error = new();
                        var documentType = await _documentTypeRepository.GetByPropertyAsync(dt => dt.Name == item.DocumentType.ToUpper());
                        if (documentType == null)
                        {
                            error.Code = ErrorMessages.BadRequest;
                            error.Message = $"Del afiliado {item.FirstName} no se encontró el tipo de documento: {item.DocumentType.ToUpper()}";
                            response.Details.Add(error);
                            continue;
                        }

                        var affiliate = await _affilliateRepository.GetByPropertyAsync(a => a.DocumentNumber == item.DocumentNumber);
                        if (affiliate != null)
                        {
                            error.Code = ErrorMessages.BadRequest;
                            error.Message = $"Ya existe un afiliado con el número de documento de {item.FirstName}";
                            response.Details.Add(error);
                            continue;
                        }

                        var client = await _clientRepository.GetByIdWithIncludeAsync(c => c.Id == item.ClientId, new List<Expression<Func<Domain.Entities.Client, object>>>
                        {
                            m => m.Policies
                        });

                        if (client == null || !client.Policies.Any())
                        {
                            error.Code = ErrorMessages.BadRequest;
                            error.Message = $"El cliente debe contactar a la ARS para afiliar a {item.FirstName}";
                            response.Details.Add(error);
                            continue;
                        }

                        var valueToAdd = _mapper.Map<Domain.Entities.Affiliate>(item);
                        valueToAdd.DocumentTypeId = documentType.Id;
                        valueToAdd.StatusId = status.Id;
                        valueToAdd.AffiliateDate = DateOnly.FromDateTime(DateTime.UtcNow);
                        valueToAdd.AffiliatePolicies = new();

                        AffiliatePolicy policy = new()
                        {
                            AffiliateId = valueToAdd.Id,
                            PolicyId = client.Policies[0].Id,
                            IsPrincipal = false,
                            StatusId = status.Id,
                            AffiliationDate = DateOnly.FromDateTime(DateTime.UtcNow)
                        };

                        valueToAdd.AffiliatePolicies.Add(policy);

                        finalList.Add(valueToAdd);
                    }
                    catch 
                    (Exception ex)
                    {
                        throw new Exception("Hubo un error creando los afiliados");
                    }
                }

                var entities = await _affilliateRepository.AddManyAsync(finalList);

                response.Affiliates = _mapper.Map<List<AddManyResponseChildDTO>>(entities);
                response.Affiliates.ForEach(a =>
                {
                    a.AffiliateStatus = "Activo";
                    a.DocumentType = command.Affiliates.Where(c => c.DocumentNumber == a.DocumentNumber).First().DocumentType;
                }); 
                response.Status = entities.Count == 0 ? "No se ingresó ningún afiliado" : "Exitoso";
                return response;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
