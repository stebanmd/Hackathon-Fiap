namespace Hackathon.Fiap.Core.Aggregates.Doctors;

public class Specialty : EntityBase, IAggregateRoot
{
    public string Name { get; set; } = default!;

    public ICollection<Doctor> Doctors { get; set; } = [];
}
