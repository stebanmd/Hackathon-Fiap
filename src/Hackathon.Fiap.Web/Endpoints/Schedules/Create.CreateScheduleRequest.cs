namespace Hackathon.Fiap.Web.Endpoints.Schedules;

public class CreateScheduleRequest
{
    public const string Route = "/schedules";

    public IList<DayOfWeek> DaysOfWeek { get; set; } = [];
    public DateOnly? Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
