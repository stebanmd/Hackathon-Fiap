namespace Hackathon.Fiap.UseCases.Schedules.Delete;
public record DeleteScheduleCommand(int DoctorId, int ScheduleId) : ICommand<Result>;
