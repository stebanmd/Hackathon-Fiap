using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

namespace Hackathon.Fiap.UseCases.Schedules.Update;

public sealed class UpdateScheduleHandler(IRepository<Doctor> repository) : ICommandHandler<UpdateScheduleCommand, Result<ScheduleDto>>
{
    private readonly IRepository<Doctor> _repository = repository;

    public async Task<Result<ScheduleDto>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (doctor is null)
        {
            return Result<ScheduleDto>.NotFound();
        }

        var schedule = doctor.Schedules.FirstOrDefault(s => s.Id == request.ScheduleId);
        if (schedule is null)
        {
            return Result<ScheduleDto>.NotFound();
        }

        schedule.Update(request.DayOfWeek, request.Day, request.Start, request.End);

        await _repository.UpdateAsync(doctor, cancellationToken);
        return Result.Success(new ScheduleDto(schedule.Id, schedule.DayOfWeek, schedule.Day, schedule.Start, schedule.End));
    }
}
