namespace Hackathon.Fiap.UseCases.Schedules.Create;

public record CreateSchedulesCommand(
    int DoctorId,
    IList<DayOfWeek> DaysOfWeek,
    DateOnly? Day,
    TimeOnly StartTime,
    TimeOnly EndTime) : ICommand<Result>;
