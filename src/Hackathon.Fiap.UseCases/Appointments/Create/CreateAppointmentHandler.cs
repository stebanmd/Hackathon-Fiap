using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Interfaces;
using Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Appointments.Create;

public class CreateAppointmentHandler(
    IMediator mediator,
    IRepository<Doctor> doctorRepository,
    IRepository<Patient> patientRepository,
    ICreateAppointmentService createAppointmentService,
    ILogger<CreateAppointmentHandler> logger) : ICommandHandler<CreateAppointmentCommand, Result<int>>
{
    private readonly IMediator _mediator = mediator;
    private readonly IRepository<Doctor> _doctorRepository = doctorRepository;
    private readonly IRepository<Patient> _patientRepository = patientRepository;
    private readonly ICreateAppointmentService _createAppointmentService = createAppointmentService;
    private readonly ILogger<CreateAppointmentHandler> _logger = logger;

    public async Task<Result<int>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Creating new appointment for doctor {DoctorId}, patient {PatientId}, date/time: {Date}", request.DoctorId, request.PatientId, request.Start);

        var doctorSpec = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _doctorRepository.FirstOrDefaultAsync(doctorSpec, cancellationToken);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found for id: {Id}", request.DoctorId);
            return Result<int>.NotFound($"Doctor not found for id: {request.DoctorId}");
        }

        var availablePeriods = await _mediator.Send(new GetAvailablePeriodsQuery(request.DoctorId, DateOnly.FromDateTime(request.Start)), cancellationToken);
        if (!availablePeriods.IsSuccess)
        {
            _logger.LogWarning("Could not check available periods for doctor {DoctorId} on {Date}. Errors {Errors}", request.DoctorId, request.Start, availablePeriods.Errors);
            return Result<int>.Error($"Could not validate doctor's availability for the given period.");
        }

        if (!availablePeriods.Value.AvailableTimes.Any(p => p == TimeOnly.FromDateTime(request.Start)))
        {
            _logger.LogWarning("Doctor {DoctorId} is not available at {Date}", request.DoctorId, request.Start);
            return Result<int>.Error($"Doctor is not available at the given period.");
        }

        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            _logger.LogError("Patient not found when creating appointment: {PatientId}", request.PatientId);
            return Result<int>.NotFound($"Patient not found for id: {request.PatientId}");
        }

        var endAppointment = request.Start.AddMinutes(doctor.GetAppointmentDuration());
        var appointmentId = await _createAppointmentService.Create(patient, doctor, request.Start, endAppointment);

        return Result.Success(appointmentId);
    }
}
