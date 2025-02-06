using Hackathon.Fiap.Api.Patients.Commons.Extensions;
using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Appointments.Create;
using Hackathon.Fiap.UseCases.Patients.GetByUserId;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

public sealed partial class Create(IMediator mediator) : Endpoint<CreateAppointmentRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post(CreateAppointmentRequest.Route);
        Roles(ApplicationRoles.Patient);

        Summary(x =>
        {
            x.ExampleRequest = new CreateAppointmentRequest(1, new(2025, 3, 3, 9, 0, 0, DateTimeKind.Unspecified));
        });
    }

    public override async Task HandleAsync(CreateAppointmentRequest req, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var patient = await _mediator.Send(new GetPatientByUserIdQuery(User.GetUserId()), ct);
        if (patient is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new CreateAppointmentCommand(patient.Id, req.DoctorId, req.Start);
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            result.Errors.ToList().ForEach(error => AddError(error));
            ThrowIfAnyErrors();
        }

        await SendOkAsync(ct);
    }
}
