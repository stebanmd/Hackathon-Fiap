using Hackathon.Fiap.UseCases.Doctors.GetByUserId;
using Hackathon.Fiap.UseCases.Schedules.Update;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

/// <summary>
/// Update a schedule for current doctor.
/// </summary>
public sealed partial class Update(IMediator mediator) : Endpoint<UpdateScheduleRequest, ScheduleRecord>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put(UpdateScheduleRequest.Route);
        Roles(ApplicationRoles.Doctor);

        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Updated schedule");
            x.Response(StatusCodes.Status400BadRequest, "Invalid request data.");
            x.Response(StatusCodes.Status404NotFound, "Doctor or schedule not found");
            x.ExampleRequest = new UpdateScheduleRequest(DayOfWeek.Monday, default, new(14, 0), new(19, 0));
        });
    }

    public override async Task HandleAsync(UpdateScheduleRequest req, CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new UpdateScheduleCommand(doctor.Id, req.ScheduleId, req.DayOfWeek, req.Day, req.Start, req.End);
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        var schedule = result.Value;
        Response = new(schedule.Id, schedule.DayOfWeek, schedule.Day, schedule.Start, schedule.End);
    }
}
