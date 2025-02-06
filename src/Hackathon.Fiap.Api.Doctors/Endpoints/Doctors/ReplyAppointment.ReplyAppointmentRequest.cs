using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public class ReplyAppointmentRequest()
{
    public const string Route = "doctors/appointments/reply";

    public int AppointmentId { get; init; }
    public AppointmentStatus Status { get; init; }
    public string? Reason { get; set; }
}
