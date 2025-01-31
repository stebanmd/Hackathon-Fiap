using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Patients;

public class Patient : EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    public string Cpf { get; private set; }

    public string UserId { get; init; }
    public ApplicationUser User { get; private set; } = default!;

    public Patient(string name, string cpf)
    {
        Name = name;
        Cpf = cpf;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }
}
