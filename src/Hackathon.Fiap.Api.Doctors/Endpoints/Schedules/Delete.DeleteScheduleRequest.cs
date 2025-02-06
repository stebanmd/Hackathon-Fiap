using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

public record DeleteScheduleRequest
{
    public const string Route = "schedules/{ScheduleId:int}";

    [FromRoute]
    public int ScheduleId { get; init; }
}
