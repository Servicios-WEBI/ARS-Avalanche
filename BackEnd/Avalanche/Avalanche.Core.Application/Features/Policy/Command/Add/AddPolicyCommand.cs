using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Policy.Command.Add
{
    public class AddPolicyCommand : IRequest<PolicyDTO>
    {
        [SwaggerParameter(Description = "Cliente")]
        [Required(ErrorMessage = "Debe de ingresar el cliente")]
        public string ClientId { get; set; }

        [SwaggerParameter(Description = "Plan")]
        [Required(ErrorMessage = "Debe de ingresar el plan")]
        public string PlanId { get; set; }
    }

    public class AddPolicyCommandHandler : IRequestHandler<AddPolicyCommand, PolicyDTO>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAffiliatePolicyRepository _affiliatePolicyRepository;
        private readonly IMapper _mapper;

        public AddPolicyCommandHandler(IPolicyRepository policyRepository, IStatusRepository statusRepository,
            IClientRepository clientRepository, IAffiliatePolicyRepository affiliatePolicyRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _statusRepository = statusRepository;
            _clientRepository = clientRepository;
            _affiliatePolicyRepository = affiliatePolicyRepository;
            _mapper = mapper;
        }

        public async Task<PolicyDTO> Handle(AddPolicyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PolicyDTO response = new();

                var client = await _clientRepository.GetByIdWithIncludeAsync(c => c.Id == command.ClientId, new List<Expression<Func<Domain.Entities.Client, object>>>
                {
                    m => m.Affiliates
                });

                if(client == null)
                {
                    throw new Exception("Ese cliente no existe en el sistema");
                }

                var status = await _statusRepository.GetByPropertyAsync(s => s.Name == Statuses.Active);
                
                //Generando numero de poliza
                string baseString = Guid.NewGuid().ToString();
                string shuffled = new string(baseString.OrderBy(_ => Guid.NewGuid()).ToArray());
                string number = new string(shuffled.Where(char.IsDigit).Take(10).ToArray());

                var valueToAdd = _mapper.Map<Domain.Entities.Policy>(command);
                valueToAdd.Number = number;
                valueToAdd.StatusId = status.Id;
                valueToAdd.EffectiveStartDate = DateOnly.FromDateTime(DateTime.UtcNow);

                var entity = await _policyRepository.AddAsync(valueToAdd);

                try
                {
                    var affiliateId = client.Affiliates.FirstOrDefault(a => a.DocumentNumber == client.DocumentNumber).Id;

                    AffiliatePolicy affiliatePolicy = new()
                    {
                        IsPrincipal = true,
                        AffiliationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        StatusId = status.Id,
                        AffiliateId = affiliateId,
                        PolicyId = entity.Id
                    };

                    await _affiliatePolicyRepository.AddAsync(affiliatePolicy);
                }
                catch (Exception ex)
                {
                    await _policyRepository.DeleteAsync(entity);
                    throw new Exception("Hubo un error al afiliar poliza");
                }
                
                response = _mapper.Map<PolicyDTO>(entity);
                response.PolicyStatus = "Activo";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se insertó correctamente la poliza" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
