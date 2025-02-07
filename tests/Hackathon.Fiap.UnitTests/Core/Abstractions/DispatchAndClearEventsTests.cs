using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;

namespace Hackathon.Fiap.UnitTests.Core.Abstractions;
public class DispatchAndClearEventsTests
{
    private class TestDomainEvent : DomainEventBase { }
    private class TestEntity : EntityBase
    {
        public void AddTestDomainEvent()
        {
            var domainEvent = new TestDomainEvent();
            RegisterDomainEvent(domainEvent);
        }
    }

    [Fact]
    public async Task CallsPublishAndClearDomainEvents()
    {
        // Arrange
        var mediatorMock = Substitute.For<IMediator>();
        var domainEventDispatcher = new MediatRDomainEventDispatcher(mediatorMock, NullLogger<MediatRDomainEventDispatcher>.Instance);
        var entity = new TestEntity();
        entity.AddTestDomainEvent();

        // Act
        await domainEventDispatcher.DispatchAndClearEvents(new List<EntityBase> { entity });

        // Assert
        await mediatorMock.Received().Publish(Arg.Any<DomainEventBase>(), Arg.Any<CancellationToken>());
        entity.DomainEvents.Should().BeEmpty();
    }
}
