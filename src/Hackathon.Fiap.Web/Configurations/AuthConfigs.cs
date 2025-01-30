using Hackathon.Fiap.Core.Aggregates.Users;
using Hackathon.Fiap.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Web.Configurations;

internal static class AuthConfigs
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization();
        services
            .AddAuthentication(IdentityConstants.BearerScheme)
            .AddCookie(IdentityConstants.ApplicationScheme)
            .AddBearerToken(IdentityConstants.BearerScheme);

        services
            .AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        return services;
    }

    public static WebApplication ConfigureAuthentication(this WebApplication app)
    {
        // Add endpoints with default implementation for the Identity Users
        app.MapIdentityApi<ApplicationUser>();

        app
          .UseAuthentication()
          .UseAuthorization();

        return app;
    }
}
