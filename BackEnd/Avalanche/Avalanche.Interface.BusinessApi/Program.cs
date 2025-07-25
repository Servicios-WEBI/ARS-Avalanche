using AutoMapper;
using Avalanche.Core.Application;
using Avalanche.Core.Application.Helpers;
using Avalanche.Core.Application.Interfaces.Repositories;
using Avalanche.Core.Application.Interfaces.Services;
using Avalanche.Core.Application.Seeds;
using Avalanche.Infrastructure.Identity;
using Avalanche.Infrastructure.Persistence;
using Avalanche.Infrastructure.Shared;
using Avalanche.Interface.BusinessApi.Extensions;
using Avalanche.Interface.BusinessApi.Hubs;
using Avalanche.Interface.BusinessApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
DotNetEnv.Env.Load();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Configure services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificDomain",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
            builder.AllowCredentials();
        });
});

builder.Services.AddLogging();
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
    options.SuppressModelStateInvalidFilter = true;
})
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "MiSesion";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();
builder.Services.AddScoped<INotificationSender, NotificationSender>();

// Build the application
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowSpecificDomain");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseHealthChecks("/health");
app.UseSession();

app.MapHub<NotificationHub>("/hub/notifications");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Seed data
if (builder.Configuration.GetValue<bool>("InitialRun"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var planPath = Path.Combine(app.Environment.ContentRootPath, "DataSeeds", "PlanSeed.csv");
        var planCoveragePath = Path.Combine(app.Environment.ContentRootPath, "DataSeeds", "PlanCoverageSeed.csv");

        try
        {
            #region Application
            var authorizationType = services.GetRequiredService<IAuthorizationTypeRepository>();
            var coverage = services.GetRequiredService<ICoverageRepository>();
            var documentType = services.GetRequiredService<IDocumentTypeRepository>();
            var institutionType = services.GetRequiredService<IInstitutionTypeRepository>();
            var plan = services.GetRequiredService<IPlanRepository>();
            var planCoverage = services.GetRequiredService<IPlanCoverageRepository>();
            var status = services.GetRequiredService<IStatusRepository>();
            var mapper = services.GetRequiredService<IMapper>();

            await DefaultAuthorizationType.SeedAsync(authorizationType);
            await DefaultCoverage.SeedAsync(coverage);
            await DefaultDocumentType.SeedAsync(documentType);
            await DefaultInstitutionType.SeedAsync(institutionType);
            await DefaultPlan.SeedAsync(plan, planPath, mapper);
            await DefaultPlanCoverage.SeedAsync(planCoverage, planCoveragePath, plan, coverage);
            await DefaultStatus.SeedAsync(status);

            #endregion

            logger.LogInformation("La carga inicial se completó satisfactoriamente");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Hubo un error completando la carga inicial");
        }
    }
}

// Run the application
app.Run();
