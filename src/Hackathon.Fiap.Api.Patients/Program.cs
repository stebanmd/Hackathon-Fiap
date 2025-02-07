using Hackathon.Fiap.Api.Patients.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger).CreateLogger<Program>();

builder.Services.AddOptionConfigs(builder.Configuration, appLogger, builder);
builder.Services.AddServiceConfigs(appLogger, builder);
builder.Services.ConfigureAuthentication();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.ShortSchemaNames = true;
    });

builder.AddServiceDefaults();

var app = builder.Build();

app.UseAppMiddleware();

app.ConfigureAuthentication()
   .MapDefaultEndpoints();


await app.RunAsync();

namespace Hackathon.Fiap.Api.Patients
{
    // Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
    public partial class Program { }
}
