namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetScheduleForPeriodSpec : Specification<Schedule>
{
    public GetScheduleForPeriodSpec(DateTime start, DateTime end)
    {
        var dayOfWeek = start.DayOfWeek;
        Query
             .Where(schedule =>
                     (schedule.DayOfWeek == dayOfWeek || schedule.Day == DateOnly.FromDateTime(start))
                         && schedule.Start <= TimeOnly.FromDateTime(start) && schedule.End >= TimeOnly.FromDateTime(end)
            );
    }
}
