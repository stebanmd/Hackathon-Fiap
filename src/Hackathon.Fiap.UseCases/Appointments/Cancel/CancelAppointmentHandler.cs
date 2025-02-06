using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Appointments.Cancel;

public sealed class CancelAppointmentHandler(IRepository<Appointment> repository, ILogger<CancelAppointmentHandler> logger) 
    : ICommandHandler<CancelAppointmentCommand, Result>
{
    private readonly IRepository<Appointment> _repository = repository;
    private readonly ILogger<CancelAppointmentHandler> _logger = logger;

    public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Canceling appointment {AppointmentId} for patient {PatientId}", request.AppointmentId, request.PatientId);

        var appointmentSpec = new GetAppointmentByIdSpec(request.AppointmentId);
        var appointment = await _repository.FirstOrDefaultAsync(appointmentSpec, cancellationToken);

        if (appointment is null)
        {
            _logger.LogWarning("Appointment {AppointmentId} not found", request.AppointmentId);
            return Result.NotFound("Appointment not found.");
        }

        if (appointment.PatientId != request.PatientId)
        {
            _logger.LogWarning("Patient {PatientId} is not the owner of appointment {AppointmentId}", request.PatientId, request.AppointmentId);
            return Result.Forbidden();
        }

        if (appointment.Status == AppointmentStatus.Canceled)
        {
            _logger.LogWarning("Appointment {AppointmentId} is already canceled", request.AppointmentId);
            return Result.Invalid(new ValidationError("Appointment is already canceled."));
        }

        // Probably need another validations to not allow cancelation of past appointments
        // or appointments that are too close to the current date

        appointment.Cancel(request.Reason);
        await _repository.UpdateAsync(appointment, cancellationToken);

        return Result.NoContent();
    }
}
