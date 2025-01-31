using Hackathon.Fiap.Core.Aggregates.Doctors.Events;

namespace Hackathon.Fiap.Core.Aggregates.Doctors.Handlers;
internal class RemoveScheduleHandler(ILogger<RemoveScheduleHandler> logger) : INotificationHandler<RemoveScheduleEvent>
{
    public Task Handle(RemoveScheduleEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Remove Schedule event for {DoctorId}", notification.Doctor.Id);
        return Task.CompletedTask;
    }
}
