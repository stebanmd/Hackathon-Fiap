using System.Reflection;
using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.UseCases.Patients.Register;

namespace Hackathon.Fiap.Api.Patients.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Patient)), // Core
            Assembly.GetAssembly(typeof(RegisterPatientCommand)) // UseCases
        };

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}
