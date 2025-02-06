using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Schedules.Update;

public sealed class UpdateScheduleHandler(IRepository<Doctor> repository, ILogger<UpdateScheduleHandler> logger) : ICommandHandler<UpdateScheduleCommand, Result<ScheduleDto>>
{
    private readonly IRepository<Doctor> _repository = repository;
    private readonly ILogger<UpdateScheduleHandler> _logger = logger;

    public async Task<Result<ScheduleDto>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Updating schedule {ScheduleId} for doctor {DoctorId}", request.ScheduleId, request.DoctorId);

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found for id {DoctorId}", request.DoctorId);
            return Result<ScheduleDto>.NotFound();
        }

        var schedule = doctor.Schedules.FirstOrDefault(s => s.Id == request.ScheduleId);
        if (schedule is null)
        {
            _logger.LogWarning("Schedule not found for id {ScheduleId}", request.ScheduleId);
            return Result<ScheduleDto>.NotFound();
        }

        schedule.Update(request.DayOfWeek, request.Day, request.Start, request.End);

        await _repository.UpdateAsync(doctor, cancellationToken);
        return Result.Success(new ScheduleDto(schedule.Id, schedule.DayOfWeek, schedule.Day, schedule.Start, schedule.End));
    }
}
