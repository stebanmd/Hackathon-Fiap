namespace Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

public class GetAppointmentsByStatusSpec : Specification<Appointment>
{
    public GetAppointmentsByStatusSpec(int doctorId, AppointmentStatus? status)
    {
        Query
            .Include(x => x.Patient)
            .Where(x => x.DoctorId == doctorId && (status == null || x.Status == status));
    }
}
