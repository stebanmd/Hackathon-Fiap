using Hackathon.Fiap.Core.Abstractions;

namespace Hackathon.Fiap.FunctionalTests;

internal class EventDispatcherTest : IDomainEventDispatcher
{
    public Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        return Task.CompletedTask;
    }
}
