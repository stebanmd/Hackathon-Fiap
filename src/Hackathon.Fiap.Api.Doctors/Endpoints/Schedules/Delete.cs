using Hackathon.Fiap.UseCases.Doctors.GetByUserId;
using Hackathon.Fiap.UseCases.Schedules.Delete;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

/// <summary>
/// Delete specific schedule from current doctor
/// </summary>
public sealed partial class Delete(IMediator mediator) : Endpoint<DeleteScheduleRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Delete(DeleteScheduleRequest.Route);
        Roles(ApplicationRoles.Doctor);

        Summary(x =>
        {
            x.Response(StatusCodes.Status204NoContent, "Schedule deleted");
            x.Response(StatusCodes.Status404NotFound, "Doctor or schedule not found");
        });
    }

    public override async Task HandleAsync(DeleteScheduleRequest req, CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new DeleteScheduleCommand(doctor.Id, req.ScheduleId);
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        await SendNoContentAsync(ct);
    }

}
