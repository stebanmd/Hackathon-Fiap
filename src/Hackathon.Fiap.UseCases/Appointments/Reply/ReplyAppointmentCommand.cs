using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Appointments.Reply;
public record ReplyAppointmentCommand(int DoctorId, int AppointmentId, AppointmentStatus Status, string? Reason) : ICommand<Result>;
