using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.Api.Patients.Commons.Extensions;
using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Appointments.Cancel;
using Hackathon.Fiap.UseCases.Patients.GetByUserId;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

/// <summary>
/// Cancel an appointment
/// </summary>
public sealed partial class Cancel(IMediator mediator) : Endpoint<CancelAppointmentRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put(CancelAppointmentRequest.Route);
        Roles(ApplicationRoles.Patient);
        Summary(x =>
        {
            x.Response(StatusCodes.Status204NoContent, "Appointment canceled successfuly");
            x.Response(StatusCodes.Status400BadRequest, "Invalid request");
            x.Response(StatusCodes.Status403Forbidden);
            x.Response(StatusCodes.Status404NotFound, "Appointment not found");
            x.ExampleRequest = new CancelAppointmentRequest()
            {
                Reason = "I am not feeling well."
            };
        });
    }

    public override async Task HandleAsync(CancelAppointmentRequest req, CancellationToken ct)
    {
        var patient = await _mediator.Send(new GetPatientByUserIdQuery(User.GetUserId()), ct);
        if (patient is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new CancelAppointmentCommand(patient.Id, req.AppointmentId, req.Reason);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
