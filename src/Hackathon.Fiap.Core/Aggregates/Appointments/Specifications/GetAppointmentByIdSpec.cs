namespace Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;

public sealed class GetAppointmentByIdSpec : Specification<Appointment>
{
    public GetAppointmentByIdSpec(int appointmentId)
    {
        Query
            .Include(app => app.Doctor)
                .ThenInclude(doc => doc.User)
            .Include(app => app.Patient)
                .ThenInclude(pat => pat.User)
            .Where(app => app.Id == appointmentId);
    }
}
