using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public record GetAppointmentsRequest(AppointmentStatus? Status)
{
    public const string Route = "/doctors/appointments";
}
