using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Patients;

public record GetAppointmentsRequest(AppointmentStatus? Status)
{
    public const string Route = "/appointments";
}
