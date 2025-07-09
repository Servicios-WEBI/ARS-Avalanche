using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Avalanche.Core.Application.Dtos.PlanCoverage
{
    public class PlanCoverageDTO
    {
        [SwaggerParameter(Description = "Limite de frecuencia por año")]
        [Required(ErrorMessage = "Debe de ingresar el monto limite")]
        public double AmountLimit { get; set; }

        [SwaggerParameter(Description = "Limite de frecuencia por año")]
        [Required(ErrorMessage = "Debe de ingresar el limite")]
        public int YearFrequencyLimit { get; set; }

        [SwaggerParameter(Description = "Porcentaje de la cobertura")]
        [Range(1, 100, ErrorMessage = "Debe ser un valor de 1 a 100")]
        public double CoveragePercentage { get; set; }

        [SwaggerParameter(Description = "Cobertura")]
        [Required(ErrorMessage = "Debe de ingresar el id de la cobertura")]
        public string CoverageId { get; set; }
    }
}
