using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Patients;

public class Patient : EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string Cpf { get; private set; }

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; private set; } = default!;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Patient() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Patient(string name, string cpf)
    {
        Name = name;
        Cpf = cpf;
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
