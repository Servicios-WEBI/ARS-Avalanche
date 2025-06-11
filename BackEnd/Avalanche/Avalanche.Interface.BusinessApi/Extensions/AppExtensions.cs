using Swashbuckle.AspNetCore.SwaggerUI;

namespace Avalanche.Interface.BusinessApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Avalanche Authentication API");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
