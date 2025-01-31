using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.Core.Aggregates.Appointments.Events;

internal class CreateAppointmentEvent(Appointment appointment, Doctor doctor) : DomainEventBase
{
    public Appointment Appointment { get; init; } = appointment;
    public Doctor Doctor { get; } = doctor;
}

