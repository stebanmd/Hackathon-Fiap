using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Appointments.Reply;

public sealed class ReplyAppointmentHandler(IRepository<Appointment> repository, ILogger<ReplyAppointmentHandler> logger)
    : ICommandHandler<ReplyAppointmentCommand, Result>
{
    private readonly IRepository<Appointment> _repository = repository;
    private readonly ILogger<ReplyAppointmentHandler> _logger = logger;

    public async Task<Result> Handle(ReplyAppointmentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Replying appointment {AppointmentId} for doctor {DoctorId}", request.AppointmentId, request.DoctorId);

        var appointmentSpec = new GetAppointmentByIdSpec(request.AppointmentId);
        var appointment = await _repository.FirstOrDefaultAsync(appointmentSpec, cancellationToken);

        if (appointment is null)
        {
            _logger.LogWarning("Appointment {AppointmentId} not found", request.AppointmentId);
            return Result.NotFound("Appointment not found.");
        }

        if (appointment.DoctorId != request.DoctorId)
        {
            _logger.LogWarning("Doctor {DoctorId} is not the owner of appointment {AppointmentId}", request.DoctorId, request.AppointmentId);
            return Result.Forbidden();
        }

        if (appointment.Status == request.Status)
        {
            _logger.LogWarning("Appointment {AppointmentId} is already {Status}", request.AppointmentId, request.Status);
            return Result.Invalid(new ValidationError($"Appointment is already {request.Status}."));
        }

        appointment.Reply(request.Status, request.Reason);
        await _repository.UpdateAsync(appointment, cancellationToken);
        return Result.NoContent();
    }
}
