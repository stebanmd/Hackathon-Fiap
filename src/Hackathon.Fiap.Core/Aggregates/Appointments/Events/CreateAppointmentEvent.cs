namespace Hackathon.Fiap.Core.Aggregates.Appointments.Events;

internal sealed class CreateAppointmentEvent(Appointment appointment) : DomainEventBase
{
    public Appointment Appointment { get; init; } = appointment;
}

