using Hackathon.Fiap.Core.Aggregates.Doctors.Events;

namespace Hackathon.Fiap.Core.Aggregates.Doctors;

public class Schedule : EntityBase
{
    public int DoctorId { get; set; }

    public DayOfWeek? DayOfWeek { get; set; }
    public DateOnly? Day { get; set; }

    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }

    public void Update(DayOfWeek? dayOfWeek, DateOnly? day, TimeOnly start, TimeOnly end)
    {
        DayOfWeek = dayOfWeek;
        Day = day;
        Start = start;
        End = end;

        RegisterDomainEvent(new ScheduleUpdatedEvent(this));
    }
}
