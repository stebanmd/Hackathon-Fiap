using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

namespace Hackathon.Fiap.UseCases.Schedules.Delete;

public sealed class DeleteScheduleHandler(IRepository<Doctor> repository) : ICommandHandler<DeleteScheduleCommand, Result>
{
    private readonly IRepository<Doctor> _repository = repository;

    public async Task<Result> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (doctor is null)
        {
            return Result.NotFound();
        }

        var schedule = doctor.Schedules.FirstOrDefault(s => s.Id == request.ScheduleId);
        if (schedule is null)
        {
            return Result.NotFound();
        }

        doctor.RemoveSchedule(schedule);
        await _repository.UpdateAsync(doctor, cancellationToken);

        return Result.Success();
    }
}
