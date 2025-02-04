namespace Hackathon.Fiap.Core.Aggregates.Doctors;
public class DoctorAppointmentConfiguration : EntityBase
{
    public int DoctorId { get; set; }

    public double Price { get; set; }
    public TimeOnly Duration { get; set; }
}
