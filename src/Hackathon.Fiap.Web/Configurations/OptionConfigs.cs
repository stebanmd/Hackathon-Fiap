﻿using Ardalis.ListStartupServices;
using Hackathon.Fiap.Infrastructure.Email;

namespace Hackathon.Fiap.Web.Configurations;

public static class OptionConfigs
{
    public static IServiceCollection AddOptionConfigs(
        this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        WebApplicationBuilder builder)
    {
        services
            .Configure<MailserverConfiguration>(configuration.GetSection("Mailserver"))
            .Configure<CookiePolicyOptions>(options =>
            {
                // Configure Web Behavior
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        if (builder.Environment.IsDevelopment())
        {
            // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(builder.Services);

                // optional - default path to view services is /listallservices - recommended to choose your own path
                config.Path = "/listservices";
            });
        }

        logger.LogInformation("{Project} were configured", "Options");

        return services;
    }
}
