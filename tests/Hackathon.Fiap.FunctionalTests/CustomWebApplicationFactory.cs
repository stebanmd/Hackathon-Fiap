using Hackathon.Fiap.Api.Doctors;
using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Infrastructure.Data;
using Hackathon.Fiap.UseCases.Doctors.AppointmentConfiguration;
using Hackathon.Fiap.UseCases.Doctors.Register;
using Hackathon.Fiap.UseCases.Schedules.Create;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Fiap.FunctionalTests;

public sealed class CustomWebApplicationFactory<TProgram> :
    WebApplicationFactory<TProgram>, IDisposable where TProgram : class
{
    private HttpClient? _customWebClient;
    public HttpClient Client
    {
        get
        {
            if (_customWebClient is null)
            {
                _customWebClient = CreateClient();
            }
            return _customWebClient;
        }
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); // will not send real emails
        var host = builder.Build();
        host.Start();

        // Get service provider.
        var serviceProvider = host.Services;

        // Create a scope to obtain a reference to the database
        // context (AppDbContext).
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();

            var mediator = scopedServices.GetRequiredService<IMediator>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

            // Reset Sqlite database for each test run
            // If using a real database, you'll likely want to remove this step.
            db.Database.EnsureDeleted();

            // Ensure the database is created.
            db.Database.EnsureCreated();

            try
            {
                // Seed the database with test data.
                SeedData.PopulateTestDataAsync(db).Wait();

                var doctorId = mediator.Send(SeedData.RegisterMockedDoctorCommand).GetAwaiter().GetResult();

                mediator.Send(
                    new RegisterAppointmentConfigurationCommand(100, 30, doctorId)).Wait();

                mediator.Send(
                    new CreateSchedulesCommand(doctorId, [], DateOnly.FromDateTime(DateTime.Today.AddDays(1)), new(8, 0), new(10, 0))).Wait();

                mediator.Send(SeedData.RegisterMockedPatientCommand).Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {exceptionMessage}", ex.Message);
            }
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Configure test dependencies here
                //// Remove the app's ApplicationDbContext registration.
                var descriptors = services.Where(d =>
                        d.ServiceType == typeof(AppDbContext) ||
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>))
                .ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                //// This should be set for each individual test run
                string inMemoryCollectionName = Guid.NewGuid().ToString();

                //// Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });

                // Add MediatR
                services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssemblies(
                        typeof(RegisterDoctorCommand).Assembly,
                        typeof(AppDbContext).Assembly);
                });

                services.AddScoped<IDomainEventDispatcher, EventDispatcherTest>();
            });
    }

    public async Task<string> AuthenticateWithDoctor()
    {
        return await Authenticate(SeedData.RegisterMockedDoctorCommand.Crm, SeedData.RegisterMockedDoctorCommand.Password);
    }

    public async Task<string> AuthenticateWithPatient()
    {
        return await Authenticate(SeedData.RegisterMockedPatientCommand.Email, SeedData.RegisterMockedPatientCommand.Password);
    }

    private async Task<string> Authenticate(string username, string password)
    {
        var client = Client;
        var content = StringContentHelpers.FromModelAsJson(new
        {
            username,
            password
        });

        var response = await client.PostAndDeserializeAsync<AccessTokenResponse>("auth/login", content);
        return response.AccessToken;
    }

    public new void Dispose()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
}


[CollectionDefinition("Doctor-Api")]
public class DoctorApiCustomWebFactory : ICollectionFixture<CustomWebApplicationFactory<Program>> { }
