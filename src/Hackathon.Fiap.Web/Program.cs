using System.Security.Claims;
using FastEndpoints.Security;
using Hackathon.Fiap.Core.Aggregates.Users;
using Hackathon.Fiap.Infrastructure.Data;
using Hackathon.Fiap.Web.Configurations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    .AddCookie()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.ShortSchemaNames = true;
    });

builder.AddServiceDefaults();

var app = builder.Build();

await app.UseAppMiddlewareAndSeedDatabase();
app.MapDefaultEndpoints();

app.MapIdentityApi<ApplicationUser>();

app
  .UseAuthentication()
  .UseAuthorization();


app.MapGet("test", async (ClaimsPrincipal claims, AppDbContext dbContext) =>
{
    var userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    return await dbContext.Users.FindAsync(userId);
})
    .RequireAuthorization();

await app.RunAsync();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }



