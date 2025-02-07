using Ardalis.ListStartupServices;

namespace Hackathon.Fiap.Api.Patients.Configurations;

public static class MiddlewareConfig
{
    public static IApplicationBuilder UseAppMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
        }
        else
        {
            app.UseDefaultExceptionHandler(); // from FastEndpoints
            app.UseHsts();
        }

        app
            .UseFastEndpoints()
            .UseSwaggerGen(); // Includes AddFileServer and static files middleware

        app.UseHttpsRedirection(); // Note this will drop Authorization headers
        return app;
    }
}
