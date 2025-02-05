﻿using Hackathon.Fiap.Core.Aggregates.Users;
using Hackathon.Fiap.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Api.Doctors.Configurations;

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
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        return services;
    }

    public static WebApplication ConfigureAuthentication(this WebApplication app)
    {
        // Add endpoints with default implementation for the Identity Users
        //app
        //    .MapIdentityApi<ApplicationUser>()
        //    .WithTags("Authentication");

        app
          .UseAuthentication()
          .UseAuthorization();

        return app;
    }
}
