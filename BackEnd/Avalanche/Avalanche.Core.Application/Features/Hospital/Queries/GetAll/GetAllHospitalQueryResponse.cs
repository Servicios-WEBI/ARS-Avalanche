using Swashbuckle.AspNetCore.Annotations;

namespace Avalanche.Core.Application.Features.Hospital.Queries.GetAll
{
    public class GetAllHospitalQueryResponse
    {
        [SwaggerSchema(Description = "Listado de hospitales")]
        public List<GetAllHospitalQueryResponseChild> Hospitals { get; set; }
    }

    public class GetAllHospitalQueryResponseChild
    {
        [SwaggerSchema(Description = "ID único de la institución")]
        public string Id { get; set; }

        [SwaggerSchema(Description = "Nombre de la institución")]
        public string Name { get; set; }

        [SwaggerSchema(Description = "Correo de la institución")]
        public string Email { get; set; }

        [SwaggerSchema(Description = "ID del tipo de institución")]
        public string InstitutionTypeId { get; set; }

        [SwaggerSchema(Description = "Tipo de institución (descripción)")]
        public string InstitutionType { get; set; }

        [SwaggerSchema(Description = "ID del estado de la institución")]
        public string StatusId { get; set; }

        [SwaggerSchema(Description = "Estado de la institución (descripción)")]
        public string Status { get; set; }
    }
}
