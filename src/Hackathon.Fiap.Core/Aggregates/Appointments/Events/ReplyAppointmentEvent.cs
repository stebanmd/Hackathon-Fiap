namespace Hackathon.Fiap.Core.Aggregates.Appointments.Events;
internal class ReplyAppointmentEvent(Appointment appointment) : DomainEventBase
{
    public Appointment Appointment { get; } = appointment;
}
