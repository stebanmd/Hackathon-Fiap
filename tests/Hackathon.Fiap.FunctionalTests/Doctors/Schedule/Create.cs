using System.Net;
using System.Net.Http.Headers;
using Hackathon.Fiap.Api.Doctors;
using Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

namespace Hackathon.Fiap.FunctionalTests.Doctors.Schedule;

[Collection("Doctor-Api")]
public class Create(CustomWebApplicationFactory<Program> factory)
{
    [Fact]
    public async Task Success()
    {
        var token = await factory.AuthenticateWithDoctor();

        var request = new HttpRequestMessage(HttpMethod.Get, CreateScheduleRequest.Route);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var obj = new CreateScheduleRequest()
        {
            DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Wednesday],
            StartTime = new TimeOnly(8, 0, 0),
            EndTime = new TimeOnly(12, 0, 0),
        };

        request.Content = StringContentHelpers.FromModelAsJson(obj);

        var result = await factory.Client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
