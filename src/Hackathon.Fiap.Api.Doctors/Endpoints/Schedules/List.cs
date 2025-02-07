using Hackathon.Fiap.UseCases.Doctors.GetByUserId;
using Hackathon.Fiap.UseCases.Schedules.List;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

/// <summary>
/// List schedules for the authenticated doctor.
/// </summary>
public sealed partial class List(IMediator mediator) : EndpointWithoutRequest<ListScheduleReponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("schedules");
        Roles(ApplicationRoles.Doctor);

        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "List of available schedules of authenticated doctor");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var result = await _mediator.Send(new ListSchedulesQuery(doctor.Id), ct);

        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        var schedules = result.Value.Select(s => new ScheduleRecord(s.Id, s.DayOfWeek, s.Day, s.Start, s.End));
        Response = new(schedules);
    }
}
