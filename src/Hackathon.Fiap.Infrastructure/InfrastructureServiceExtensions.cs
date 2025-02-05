using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Interfaces;
using Hackathon.Fiap.Core.Services;
using Hackathon.Fiap.Infrastructure.Authorization;
using Hackathon.Fiap.Infrastructure.Data;
using Hackathon.Fiap.Infrastructure.Data.Queries;
using Hackathon.Fiap.UseCases.Contributors.List;
using Hackathon.Fiap.UseCases.Doctors.List;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager config, ILogger logger, string environmentName)
    {
        if (environmentName == "Testing")
        {
            // do not configure a DbContext here for testing - it's configured in CustomWebApplicationFactory

            services.AddScoped<IListContributorsQueryService, FakeListContributorsQueryService>();
            services.AddScoped<IListDoctorsQueryService, FakeListDoctorsQueryService>();
        }
        else
        {
            AddDbContext(services, config);
            services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
            services.AddScoped<IListDoctorsQueryService, ListDoctorsQueryService>();
        }

        services.AddScoped<IDeleteContributorService, DeleteContributorService>();

        services
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.AddScoped<IAuthorizationHandler, PatientMustBeDataOwnerHandler>();
        services.AddScoped<IAuthorizationHandler, DoctorMustBeDataOwnerHandler>();

        logger.LogInformation("{Project} services registered", "Infrastructure");
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("hackathonDb");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}
