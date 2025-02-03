namespace Hackathon.Fiap.Core.Aggregates.Doctors;
public class Schedule : EntityBase
{
    public int DoctorId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }
    public DateOnly? Day { get; set; }

    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}
