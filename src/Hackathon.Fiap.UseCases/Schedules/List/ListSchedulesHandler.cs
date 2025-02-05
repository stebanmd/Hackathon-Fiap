using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

namespace Hackathon.Fiap.UseCases.Schedules.List;

public sealed class ListSchedulesHandler(IRepository<Doctor> repository) : IQueryHandler<ListSchedulesQuery, Result<IEnumerable<ScheduleDto>>>
{
    private readonly IRepository<Doctor> _repository = repository;

    public async Task<Result<IEnumerable<ScheduleDto>>> Handle(ListSchedulesQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (doctor is null)
        {
            return Result<IEnumerable<ScheduleDto>>.NotFound();
        }

        return Result.Success(doctor.Schedules.Select(schedule => new ScheduleDto
        (schedule.Id, schedule.DayOfWeek, schedule.Day, schedule.Start, schedule.End)));
    }
}
