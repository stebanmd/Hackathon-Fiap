using System.Reflection;
using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.UseCases.Doctors.Register;

namespace Hackathon.Fiap.Api.Doctors.Configurations;

public static class MediatrConfigs
{
    public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Doctor)), // Core
            Assembly.GetAssembly(typeof(RegisterDoctorCommand)) // UseCases
        };

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        return services;
    }
}
