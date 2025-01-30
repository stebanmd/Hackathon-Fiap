using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Interfaces;
using Hackathon.Fiap.Core.Services;
using Hackathon.Fiap.Infrastructure.Data;
using Hackathon.Fiap.Infrastructure.Data.Queries;
using Hackathon.Fiap.UseCases.Contributors.List;

namespace Hackathon.Fiap.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager config, ILogger logger, string environmentName)
    {
        if (environmentName == "Testing")
        {
            // do not configure a DbContext here for testing - it's configured in CustomWebApplicationFactory

            services.AddScoped<IListContributorsQueryService, FakeListContributorsQueryService>();
        }
        else
        {
            AddDbContext(services, config);
            services.AddScoped<IListContributorsQueryService, ListContributorsQueryService>();
        }


        services.AddScoped<IDeleteContributorService, DeleteContributorService>();

        services
            .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        logger.LogInformation("{Project} services registered", "Infrastructure");
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("hackathonDb");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    }
}
