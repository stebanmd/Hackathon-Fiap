using Ardalis.ListStartupServices;
using Hackathon.Fiap.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Fiap.Web.Configurations;

public static class MiddlewareConfig
{
    public static async Task<IApplicationBuilder> UseAppMiddlewareAndSeedDatabase(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
            await ApplyMigrationsAndSeedDatabase(app);
        }
        else
        {
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
        }

        await SeedIdentityData(app);
        app
            .UseFastEndpoints()
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection(); // Note this will drop Authorization headers
        return app;
    }

    static async Task ApplyMigrationsAndSeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();
            await SeedData.InitializeAsync(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
        }
    }

    static async Task SeedIdentityData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roleExist = await roleManager.RoleExistsAsync(ApplicationRoles.Admin);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Doctor));
            await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Patient));
        }
    }
}
