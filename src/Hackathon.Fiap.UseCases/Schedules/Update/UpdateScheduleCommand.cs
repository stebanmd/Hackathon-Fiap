namespace Hackathon.Fiap.UseCases.Schedules.Update;
public record UpdateScheduleCommand(
    int DoctorId,
    int ScheduleId,
    DayOfWeek? DayOfWeek,
    DateOnly? Day,
    TimeOnly Start,
    TimeOnly End) : ICommand<Result<ScheduleDto>>;
