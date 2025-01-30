namespace Hackathon.Fiap.Core.Abstractions;
public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
}
