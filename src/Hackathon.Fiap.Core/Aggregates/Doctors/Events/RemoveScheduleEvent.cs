namespace Hackathon.Fiap.Core.Aggregates.Doctors.Events;

internal class RemoveScheduleEvent(Doctor doctor, Schedule schedule) : DomainEventBase
{
    public Doctor Doctor { get; } = doctor;
    public Schedule Schedule { get; } = schedule;
}
