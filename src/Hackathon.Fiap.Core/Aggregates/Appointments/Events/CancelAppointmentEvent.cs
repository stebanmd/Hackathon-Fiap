namespace Hackathon.Fiap.Core.Aggregates.Appointments.Events;
internal class CancelAppointmentEvent(Appointment appointment) : DomainEventBase
{
    public Appointment Appointment { get; } = appointment;
}
