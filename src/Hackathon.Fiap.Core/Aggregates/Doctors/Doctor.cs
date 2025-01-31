using Hackathon.Fiap.Core.Aggregates.Doctors.Events;
using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Doctors;
public class Doctor : EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string Crm { get; private set; }
    public string Cpf { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public string UserId { get; init; }
    public ApplicationUser User { get; private set; } = default!;

    public List<Schedule> Schedules { get; private set; } = [];

    public Doctor(string name, string cpf, string crm)
    {
        Name = name;
        Crm = crm;
        Cpf = cpf;
    }

    public void AddSchedule(Schedule schedule)
    {
        Schedules.Add(schedule);
    }

    public void RemoveSchedule(Schedule schedule)
    {
        Schedules.Remove(schedule);
        RegisterDomainEvent(new RemoveScheduleEvent(this, schedule));
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }
}
