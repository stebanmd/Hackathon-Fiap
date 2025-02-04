namespace Hackathon.Fiap.UseCases.Doctors.AppointmentConfiguration;

public record RegisterAppointmentConfigurationCommand(double Price, double Duration, int DoctorId) : ICommand<Result>;
