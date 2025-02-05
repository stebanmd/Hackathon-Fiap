namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetScheduleByDateSpec : Specification<Schedule>
{
    public GetScheduleByDateSpec(DateOnly date)
    {
        var dayOfWeek = date.DayOfWeek;
        Query
             .Where(schedule => schedule.DayOfWeek == dayOfWeek || schedule.Day == date);
    }
}
