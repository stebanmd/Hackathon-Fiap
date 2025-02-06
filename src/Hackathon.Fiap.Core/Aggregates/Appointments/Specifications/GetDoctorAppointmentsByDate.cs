namespace Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
public class GetDoctorAppointmentsByDate : Specification<Appointment>
{
    public GetDoctorAppointmentsByDate(int doctorId, DateOnly date)
    {
        var startDate = date.ToDateTime(new(0, 0));
        Query
          .Include(app => app.Doctor)
          .Include(app => app.Patient)
          .Where(app => app.DoctorId == doctorId && app.Start.Date == startDate && app.Status != AppointmentStatus.Canceled);
    }
}
