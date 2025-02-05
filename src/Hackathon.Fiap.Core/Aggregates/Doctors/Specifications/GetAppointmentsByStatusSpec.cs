using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetAppointmentsByStatusSpec : Specification<Appointment>
{
    public GetAppointmentsByStatusSpec(AppointmentStatus? status)
    {
        Query
             .Where(x => status == null || x.Status == status);
    }
}
