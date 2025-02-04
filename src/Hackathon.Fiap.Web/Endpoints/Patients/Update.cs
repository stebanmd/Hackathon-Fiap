using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.Infrastructure.Authorization;
using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Patients.Update;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Web.Endpoints.Patients;

public class Update(IMediator mediator, IAuthorizationService authService) : Endpoint<UpdatePatientRequest>
{
    private readonly IMediator _mediator = mediator;
    private readonly IAuthorizationService _authService = authService;

    public override void Configure()
    {
        Put(UpdatePatientRequest.Route);
        Roles(ApplicationRoles.Patient, ApplicationRoles.Admin);
        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Patient updated successfully.");
            x.Response(StatusCodes.Status400BadRequest, "Invalid Payload.");
            x.Response(StatusCodes.Status403Forbidden, "Current user does not have access to this resource.");
            x.Response(StatusCodes.Status404NotFound, "Patient not found.");
            x.ExampleRequest = new UpdatePatientRequest("John Doe");
        });
    }

    public override async Task HandleAsync(UpdatePatientRequest req, CancellationToken ct)
    {
        var authResult = await _authService.AuthorizeAsync(User, null, new PatientMustBeDataOwnerRequirement(req.Id));
        if (!authResult.Succeeded)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new UpdatePatientCommand(req.Id, req.Name);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
