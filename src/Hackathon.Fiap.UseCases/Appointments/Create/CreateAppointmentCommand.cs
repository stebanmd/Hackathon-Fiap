namespace Hackathon.Fiap.UseCases.Appointments.Create;
public record CreateAppointmentCommand(int PatientId, int DoctorId, DateTime Start) : ICommand<Result<int>>;

