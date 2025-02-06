namespace Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

public class GetDoctorAppointmentsByStatusSpec : Specification<Appointment>
{
    public GetDoctorAppointmentsByStatusSpec(int doctorId, AppointmentStatus? status)
    {
        Query
            .Include(x => x.Patient)
            .Where(x => x.DoctorId == doctorId && (status == null || x.Status == status));
    }
}
