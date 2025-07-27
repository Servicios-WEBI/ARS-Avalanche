using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Dtos.Affiliate
{
    public class AffiliateRequestDTO
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

        [SwaggerParameter(Description = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public DateOnly BirthDate { get; set; }

        [SwaggerParameter(Description = "Género")]
        [Required(ErrorMessage = "Debe ingresar el género")]
        public string Gender { get; set; }

        [SwaggerParameter(Description = "Cliente")]
        [Required(ErrorMessage = "Debe ingresar el cliente")]
        public string ClientId { get; set; }
    }
}
