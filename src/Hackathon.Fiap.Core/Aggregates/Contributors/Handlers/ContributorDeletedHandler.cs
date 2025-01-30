using Hackathon.Fiap.Core.Aggregates.Contributors.Events;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Aggregates.Contributors.Handlers;

/// <summary>
/// NOTE: Internal because ContributorDeleted is also marked as internal.
/// </summary>
internal class ContributorDeletedHandler(ILogger<ContributorDeletedHandler> logger, IEmailSender emailSender) : INotificationHandler<ContributorDeletedEvent>
{
    public async Task Handle(ContributorDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Contributed Deleted event for {ContributorId}", domainEvent.ContributorId);

        await emailSender.SendEmailAsync("to@test.com",
                                         "from@test.com",
                                         "Contributor Deleted",
                                         $"Contributor with id {domainEvent.ContributorId} was deleted.");
    }
}
