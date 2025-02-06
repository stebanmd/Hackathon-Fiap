using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.GetByUserId;
using Hackathon.Fiap.UseCases.Schedules.Create;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

/// <summary>
/// Create a schedule for a doctor informing when they will be available
/// </summary>
public partial class Create(IMediator mediator) : Endpoint<CreateScheduleRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post(CreateScheduleRequest.Route);
        Roles(ApplicationRoles.Doctor);
        Summary(x =>
        {
            x.ExampleRequest = new CreateScheduleRequest()
            {
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Tuesday],
                StartTime = new(8, 0),
                EndTime = new(14, 0)
            };
        });
    }

    public override async Task HandleAsync(CreateScheduleRequest req, CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new CreateSchedulesCommand(doctor.Id, req.DaysOfWeek, req.Day, req.StartTime, req.EndTime);
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            await SendResultAsync(result.ToMinimalApiResult());
            return;
        }

        await SendOkAsync(ct);
    }
}
