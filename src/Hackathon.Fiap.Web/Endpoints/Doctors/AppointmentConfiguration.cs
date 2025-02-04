using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.UseCases.Doctors.AppointmentConfiguration;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

/// <summary>
/// Register a new doctor appointment configuration
/// </summary>
public partial class AppointmentConfiguration(IMediator mediator) : Endpoint<RegisterAppointmentConfigurationRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put(RegisterAppointmentConfigurationRequest.Route);
        AllowAnonymous();
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
        var command = new RegisterAppointmentConfigurationCommand(req.Price, req.Duration, req.Id);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
