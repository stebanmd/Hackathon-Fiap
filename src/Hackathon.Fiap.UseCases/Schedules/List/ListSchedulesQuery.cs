namespace Hackathon.Fiap.UseCases.Schedules.List;
public record ListSchedulesQuery(int DoctorId) : IQuery<Result<IEnumerable<ScheduleDto>>>;

