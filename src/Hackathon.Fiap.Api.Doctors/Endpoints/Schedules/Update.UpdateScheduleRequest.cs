using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

public record UpdateScheduleRequest(DayOfWeek? DayOfWeek, DateOnly? Day, TimeOnly Start, TimeOnly End)
{
    public const string Route = "schedules/{ScheduleId:int}";

    [FromRoute]
    public int ScheduleId { get; init; }
}

