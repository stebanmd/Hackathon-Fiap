using System.Net;
using Hackathon.Fiap.Api.Doctors;
using Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

namespace Hackathon.Fiap.FunctionalTests.Doctors.Doctors;

[Collection("Doctor-Api")]
public class Specialties(CustomWebApplicationFactory<Program> factory) 
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        var result = await _httpClient.GetAsync("doctors/specialties");

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task List()
    {
        var result = await _httpClient.GetAndDeserializeAsync<IEnumerable<GetSpecialtiesResponse>>("doctors/specialties");

        // Assert
        Assert.True(result.Count() == 3);
    }
}
