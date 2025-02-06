namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

public class CancelAppointmentRequest()
{
    public const string Route = "appointments/{AppointmentId:int}/cancel";

    public int AppointmentId { get; init; }

    public string? Reason { get; set; }
}
