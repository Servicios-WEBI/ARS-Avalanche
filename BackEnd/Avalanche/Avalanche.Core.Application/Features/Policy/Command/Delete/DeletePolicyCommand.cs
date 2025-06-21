using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Policy;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Features.Policy.Command.Delete
{
    public class DeletePolicyCommand : IRequest<PolicyDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, PolicyDTO>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public DeletePolicyCommandHandler(IPolicyRepository policyRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public async Task<PolicyDTO> Handle(DeletePolicyCommand command, CancellationToken cancellationToken)
        {
            try
            {
                PolicyDTO response = new();
                var valueToDelete = await _policyRepository.GetByIdAsync(command.Id);

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _policyRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<PolicyDTO>(valueToDelete);
                response.PolicyStatus = "Eliminado";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente la poliza" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
