using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.Api.Doctors.Commons.Extensions;
using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Appointments.Reply;
using Hackathon.Fiap.UseCases.Doctors.GetByUserId;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

/// <summary>
/// Reply an appointment
/// </summary>
public sealed partial class ReplyAppointment(IMediator mediator) : Endpoint<ReplyAppointmentRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put(ReplyAppointmentRequest.Route);
        Roles(ApplicationRoles.Doctor);
        Summary(x =>
        {
            x.Response(StatusCodes.Status204NoContent, "Appointment replied successfuly");
            x.Response(StatusCodes.Status400BadRequest, "Invalid request");
            x.Response(StatusCodes.Status403Forbidden);
            x.Response(StatusCodes.Status404NotFound, "Appointment not found");
        });
    }

    public override async Task HandleAsync(ReplyAppointmentRequest req, CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new ReplyAppointmentCommand(doctor.Id, req.AppointmentId, req.Status, req.Reason);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
