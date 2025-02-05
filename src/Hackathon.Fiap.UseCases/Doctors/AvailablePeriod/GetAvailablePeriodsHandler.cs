using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;

public class GetAvailablePeriodsHandler(
    IRepository<Doctor> doctorRepository,
    IRepository<Appointment> appointmentRepository,
    ILogger<GetAvailablePeriodsHandler> logger) : IQueryHandler<GetAvailablePeriodsQuery, Result<AvailableAppointmentsDto>>
{
    private readonly IRepository<Doctor> _doctorRepository = doctorRepository;
    private readonly IRepository<Appointment> _appointmentRepository = appointmentRepository;
    private readonly ILogger<GetAvailablePeriodsHandler> _logger = logger;

    public async Task<Result<AvailableAppointmentsDto>> Handle(GetAvailablePeriodsQuery request, CancellationToken cancellationToken)
    {
        var specDoctor = new GetDoctorByIdSpec(request.DoctorId);
        var doctor = await _doctorRepository.FirstOrDefaultAsync(specDoctor, cancellationToken);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found");
            return Result<AvailableAppointmentsDto>.NotFound("Doctor not found");
        }

        if (doctor.AppointmentConfiguration is null)
        {
            _logger.LogWarning("Doctor does not have appointment configurated");
            return Result<AvailableAppointmentsDto>.Error("Doctor does not have appointment configurated");
        }

        var specAppointments = new GetDoctorAppointmentsByDate(request.DoctorId, request.Date);
        var appointments = await _appointmentRepository.ListAsync(specAppointments, cancellationToken);

        var availableTimes = GetTimeAvailables(doctor, request.Date);

        foreach (var appointment in appointments)
        {
            availableTimes.Remove(TimeOnly.FromDateTime(appointment.Start));
        }

        var result = new AvailableAppointmentsDto(doctor.AppointmentConfiguration.Price, availableTimes);
        return Result<AvailableAppointmentsDto>.Success(result);
    }

    private List<TimeOnly> GetTimeAvailables(Doctor doctor, DateOnly date)
    {
        var schedulesSpec = new GetScheduleByDateSpec(date);
        var schedules = schedulesSpec.Evaluate(doctor.Schedules);

        if (!schedules.Any())
        {
            _logger.LogWarning("Doctor {Id} does not have schedule for the date {Date}", doctor.Id, date);
            return [];
        }

        var duration = doctor.AppointmentConfiguration!.Duration;
        var availableTimes = new HashSet<TimeOnly>();

        foreach (var schedule in schedules)
        {
            var time = schedule.Start;
            while (time < schedule.End)
            {
                availableTimes.Add(time);
                time = time.AddMinutes(duration);
            }
        }

        return [.. availableTimes];
    }
}
