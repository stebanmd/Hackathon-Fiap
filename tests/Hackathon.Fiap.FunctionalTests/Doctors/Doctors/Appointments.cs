using System.Net;
using System.Net.Http.Headers;
using Hackathon.Fiap.Api.Doctors;
using Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

namespace Hackathon.Fiap.FunctionalTests.Doctors.Doctors;

[Collection("Doctor-Api")]
public class Appointments(CustomWebApplicationFactory<Program> factory) 
{
    [Fact]
    public async Task Success()
    {
        var token = await factory.AuthenticateWithDoctor();
        
        var request = new HttpRequestMessage(HttpMethod.Get, GetAppointmentsRequest.Route);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await factory.Client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
