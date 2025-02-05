using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Schedules.Create;

public class CreateSchedulesHandler(IRepository<Doctor> repository, ILogger<CreateSchedulesHandler> logger) : ICommandHandler<CreateSchedulesCommand, Result>
{
    private readonly IRepository<Doctor> _repository = repository;
    private readonly ILogger<CreateSchedulesHandler> _logger = logger;

    public async Task<Result> Handle(CreateSchedulesCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Registering new schedule(s) for doctor: {Id}", request.DoctorId);
        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found: {Id}", request.DoctorId);
            return Result.NotFound();
        }

        if (request.DaysOfWeek.Count > 0)
        {
            foreach (var day in request.DaysOfWeek)
            {
                var schedule = new Schedule()
                {
                    DayOfWeek = day,
                    Start = request.StartTime,
                    End = request.EndTime,
                };
                doctor.AddSchedule(schedule);
            }
        }
        else
        {
            var schedule = new Schedule()
            {
                Day = request.Day,
                Start = request.StartTime,
                End = request.EndTime,
            };
            doctor.AddSchedule(schedule);
        }

        await _repository.UpdateAsync(doctor, cancellationToken);
        return Result.Success();
    }
}
