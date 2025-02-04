using Hackathon.Fiap.Core.Aggregates.Users;

namespace Hackathon.Fiap.Core.Aggregates.Patients;

public class Patient(string name, string cpf) : EntityBase, IAggregateRoot
{
    public string Name { get; private set; } = name;
    public string Cpf { get; private set; } = cpf;

    public string UserId { get; set; } = default!;
    public ApplicationUser User { get; private set; } = default!;

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void SetUser(ApplicationUser user)
    {
        User = user;
    }
}
