using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.UseCases.Patients.Update;

namespace Hackathon.Fiap.Web.Endpoints.Patients;

public class Update(IMediator mediator) : Endpoint<UpdatePatientRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put(UpdatePatientRequest.Route);
        Roles(ApplicationRoles.Patient, ApplicationRoles.Admin);
        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Patient updated successfully.");
            x.Response(StatusCodes.Status400BadRequest, "Invalid Payload");
            x.Response(StatusCodes.Status404NotFound, "Patient not found.");
            x.ExampleRequest = new UpdatePatientRequest("John Doe");
        });
    }

    public override async Task HandleAsync(UpdatePatientRequest req, CancellationToken ct)
    {
        var command = new UpdatePatientCommand(req.Id, req.Name);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
