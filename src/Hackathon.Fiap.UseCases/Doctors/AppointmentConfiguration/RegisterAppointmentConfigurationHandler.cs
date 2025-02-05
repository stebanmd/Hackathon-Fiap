using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Doctors.AppointmentConfiguration;

public class RegisterAppointmentConfigurationHandler(
    ILogger<RegisterAppointmentConfigurationHandler> logger,
    IRepository<Doctor> repository) : ICommandHandler<RegisterAppointmentConfigurationCommand, Result>
{
    private readonly ILogger<RegisterAppointmentConfigurationHandler> _logger = logger;
    private readonly IRepository<Doctor> _repository = repository;

    public async Task<Result> Handle(RegisterAppointmentConfigurationCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Registering new appointment configuration for doctor: {Id}", request.DoctorId);

        var spec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found: {Id}", request.DoctorId);
            return Result.NotFound();
        }

        doctor.SetAppointmentConfiguration(new()
        {
            Price = request.Price,
            Duration = request.Duration,
        });

        await _repository.UpdateAsync(doctor, cancellationToken);
        return Result.Success();
    }
}
