using Hackathon.Fiap.Infrastructure.Authorization;
using Hackathon.Fiap.UseCases.Doctors.AppointmentConfiguration;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

/// <summary>
/// Register a new doctor appointment configuration
/// </summary>
public partial class AppointmentConfiguration(IMediator mediator, IAuthorizationService authService) : Endpoint<RegisterAppointmentConfigurationRequest>
{
    private readonly IMediator _mediator = mediator;
    private readonly IAuthorizationService _authService = authService;

    public override void Configure()
    {
        Put(RegisterAppointmentConfigurationRequest.Route);
        Roles(ApplicationRoles.Doctor);
        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Doctor appointment configuration registered successfully");
            x.Response(StatusCodes.Status404NotFound, "Doctor not found");
            x.ExampleRequest = new RegisterAppointmentConfigurationRequest
            (
                100.00,
                60
            );
        });
    }

    public override async Task HandleAsync(RegisterAppointmentConfigurationRequest req, CancellationToken ct)
    {
        var authResult = await _authService.AuthorizeAsync(User, null, new DoctorMustBeDataOwnerRequirement(req.Id));
        if (!authResult.Succeeded)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var command = new RegisterAppointmentConfigurationCommand(req.Price, req.Duration, req.Id);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
