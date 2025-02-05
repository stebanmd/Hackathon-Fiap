using Hackathon.Fiap.Api.Doctors.Commons.Validators;
using Hackathon.Fiap.Core.Interfaces;
using Hackathon.Fiap.Infrastructure;
using Hackathon.Fiap.Infrastructure.Email;

namespace Hackathon.Fiap.Api.Doctors.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, ILogger logger, WebApplicationBuilder builder)
    {
        services
            .AddInfrastructureServices(builder.Configuration, logger, builder.Environment.EnvironmentName)
            .AddMediatrConfigs();

        if (builder.Environment.EnvironmentName == "Testing")
        {
            services.AddScoped<IEmailSender, FakeEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, MimeKitEmailSender>();
        }

        builder.Services.AddScoped<PasswordValidator>();

        logger.LogInformation("{Project} services registered", "Mediatr and Email Sender");
        return services;
    }
}
