namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

public sealed record CreateAppointmentRequest(int DoctorId, DateTime Start)
{
    public const string Route = "/appointments";
}
