namespace Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

public class GetPatientAppointmentsByStatusSpec : Specification<Appointment>
{
    public GetPatientAppointmentsByStatusSpec(int patientId, AppointmentStatus? status)
    {
        Query
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Where(x => x.PatientId == patientId && (status == null || x.Status == status));
    }
}
