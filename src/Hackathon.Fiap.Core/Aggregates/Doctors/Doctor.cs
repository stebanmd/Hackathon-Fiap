using Hackathon.Fiap.Core.Aggregates.Doctors.Events;
using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Doctors;

public class Doctor(string name, string cpf, string crm) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Crm { get; private set; } = crm;
    public string Cpf { get; private set; } = cpf;

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; private set; } = default!;

    public List<Schedule> Schedules { get; private set; } = [];

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

    public void SetUser(ApplicationUser user)
    {
        User = user;
    }
}
