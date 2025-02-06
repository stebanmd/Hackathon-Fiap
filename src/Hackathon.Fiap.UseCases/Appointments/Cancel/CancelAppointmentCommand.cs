namespace Hackathon.Fiap.UseCases.Appointments.Cancel;
public record CancelAppointmentCommand(int PatientId, int AppointmentId, string Reason) : ICommand<Result>;
