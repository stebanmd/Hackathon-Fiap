using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetAppointmentsByStatusSpec : Specification<Appointment>
{
    public GetAppointmentsByStatusSpec(int doctorId, AppointmentStatus? status)
    {
        Query
             .Where(x => x.DoctorId == doctorId && (status == null || x.Status == status));
    }
}
