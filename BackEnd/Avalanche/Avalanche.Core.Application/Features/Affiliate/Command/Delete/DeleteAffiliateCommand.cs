using AutoMapper;
using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;
using Avalanche.Core.Application.Dtos.Affiliate;
using Avalanche.Core.Application.Interfaces.Repositories;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Avalanche.Core.Application.Features.Affiliate.Command.Delete
{
    public class DeleteAffiliateCommand : IRequest<AffiliateDTO>
    {
        [SwaggerParameter(Description = "Id")]
        [Required(ErrorMessage = "Debe de ingresar el identificador único")]
        public string Id { get; set; }
    }

    public class DeleteAffiliateCommandHandler : IRequestHandler<DeleteAffiliateCommand, AffiliateDTO>
    {
        private readonly IAffiliateRepository _affilliateRepository;
        private readonly IMapper _mapper;

        public DeleteAffiliateCommandHandler(IAffiliateRepository affilliateRepository, IMapper mapper)
        {
            _affilliateRepository = affilliateRepository;
            _mapper = mapper;
        }

        public async Task<AffiliateDTO> Handle(DeleteAffiliateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                AffiliateDTO response = new();
                var valueToDelete = await _affilliateRepository.GetByIdWithIncludeAsync(e => e.Id == command.Id, new List<Expression<Func<Domain.Entities.Affiliate, object>>>
                {
                    m => m.DocumentType
                });

                if (valueToDelete == null)
                    throw new Exception(ErrorMessages.NotFound);

                await _affilliateRepository.DeleteAsync(valueToDelete);

                response = _mapper.Map<AffiliateDTO>(valueToDelete);
                response.DocumentType = valueToDelete.DocumentType.Name;
                response.AffiliateStatus = "Eliminado";
                response.Status = "Exitoso";
                response.Details = [new ErrorDetailsDTO { Code = "000", Message = "Se eliminó correctamente el afiliado" }];
                return response;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
