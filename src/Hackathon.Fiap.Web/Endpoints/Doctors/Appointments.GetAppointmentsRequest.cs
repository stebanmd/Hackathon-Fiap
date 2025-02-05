using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record GetAppointmentsRequest(AppointmentStatus? Status)
{
    public const string Route = "/doctors/appointments";
}
