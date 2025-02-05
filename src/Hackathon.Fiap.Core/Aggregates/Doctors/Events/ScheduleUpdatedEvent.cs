namespace Hackathon.Fiap.Core.Aggregates.Doctors.Events;
public class ScheduleUpdatedEvent(Schedule schedule) : DomainEventBase
{
    public Schedule Schedule { get; } = schedule;
}
