using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Schedules.List;

public sealed class ListSchedulesHandler(IRepository<Doctor> repository, ILogger<ListSchedulesHandler> logger) : IQueryHandler<ListSchedulesQuery, Result<IEnumerable<ScheduleDto>>>
{
    private readonly IRepository<Doctor> _repository = repository;
    private readonly ILogger<ListSchedulesHandler> _logger = logger;

    public async Task<Result<IEnumerable<ScheduleDto>>> Handle(ListSchedulesQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Listing schedules for doctor {DoctorId}", request.DoctorId);

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found for id {DoctorId}", request.DoctorId);
            return Result<IEnumerable<ScheduleDto>>.NotFound();
        }

        return Result.Success(doctor.Schedules.Select(schedule => new ScheduleDto
        (schedule.Id, schedule.DayOfWeek, schedule.Day, schedule.Start, schedule.End)));
    }
}
