using System.Net;
using Hackathon.Fiap.Api.Doctors;
using Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

namespace Hackathon.Fiap.FunctionalTests.Doctors.Doctors;

[Collection("Doctor-Api")]
public class Register(CustomWebApplicationFactory<Program> factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        // Arrange
        var request = new RegisterDoctorRequest("John Smith", "12345678909", "987564-RS", "test@test.com", "str0ngP@assword")
        {
            ConfirmPassword = "str0ngP@assword",
        };
        var content = StringContentHelpers.FromModelAsJson(request);

        // Act
        var result = await _httpClient.PostAndDeserializeAsync<int>(RegisterDoctorRequest.Route, content);

        // Assert
        Assert.True(result > 0);
    }

    [Fact]
    public async Task BadRequest_WeakPassword()
    {
        // Arrange
        var request = new RegisterDoctorRequest("John Smith", "12345678909", "987564-RS", "test@test.com", "senha")
        {
            ConfirmPassword = "senha",
        };
        var content = StringContentHelpers.FromModelAsJson(request);

        // Act
        // Assert
        var result = await _httpClient.PostAndEnsureBadRequestAsync(RegisterDoctorRequest.Route, content);
        Assert.Contains("Password", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Conflict()
    {
        // Arrange
        var request1 = new RegisterDoctorRequest("Jonathan P. da Rocha", "12345678909", "852654-RS", "jonathan_rocha@test.com", "str0ngP@assword")
        {
            ConfirmPassword = "str0ngP@assword",
        };

        var request2 = request1 with { Name = "Second doctor" };

        var content1 = StringContentHelpers.FromModelAsJson(request1);
        var content2 = StringContentHelpers.FromModelAsJson(request2);

        // Act
        var result1 = await _httpClient.PostAndDeserializeAsync<int>(RegisterDoctorRequest.Route, content1);
        var result2 = await _httpClient.PostAsync(RegisterDoctorRequest.Route, content2);

        // Assert
        Assert.True(result1 > 0);
        Assert.Equal(HttpStatusCode.Conflict, result2.StatusCode);
    }
}
