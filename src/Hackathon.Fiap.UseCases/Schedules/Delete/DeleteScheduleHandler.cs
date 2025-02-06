using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Schedules.Delete;

public sealed class DeleteScheduleHandler(IRepository<Doctor> repository, ILogger<DeleteScheduleHandler> logger) : ICommandHandler<DeleteScheduleCommand, Result>
{
    private readonly IRepository<Doctor> _repository = repository;
    private readonly ILogger<DeleteScheduleHandler> _logger = logger;

    public async Task<Result> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Deleting schedule {ScheduleId} for doctor {DoctorId}", request.ScheduleId, request.DoctorId);

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found for id {DoctorId}", request.DoctorId);
            return Result.NotFound();
        }

        var schedule = doctor.Schedules.FirstOrDefault(s => s.Id == request.ScheduleId);
        if (schedule is null)
        {
            _logger.LogWarning("Schedule not found for id {ScheduleId}", request.ScheduleId);
            return Result.NotFound();
        }

        doctor.RemoveSchedule(schedule);
        await _repository.UpdateAsync(doctor, cancellationToken);

        return Result.Success();
    }
}
