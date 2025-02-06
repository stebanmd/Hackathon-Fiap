namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

public record ScheduleRecord(int Id, DayOfWeek? DayOfWeek, DateOnly? Day, TimeOnly Start, TimeOnly End);
