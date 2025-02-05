namespace Hackathon.Fiap.Web.Endpoints.Appointments;

public sealed record CreateAppointmentRequest(int DoctorId, DateTime Start)
{
    public const string Route = "/appointments";
}
