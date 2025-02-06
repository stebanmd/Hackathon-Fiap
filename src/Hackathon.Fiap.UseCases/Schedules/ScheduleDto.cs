namespace Hackathon.Fiap.UseCases.Schedules;
public record ScheduleDto(int Id, DayOfWeek? DayOfWeek, DateOnly? Day, TimeOnly Start, TimeOnly End);
