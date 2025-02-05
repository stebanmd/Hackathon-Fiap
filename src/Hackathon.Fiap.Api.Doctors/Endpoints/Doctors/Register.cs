using Hackathon.Fiap.UseCases.Doctors.Register;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

/// <summary>
/// Register a new doctor in the system
/// </summary>
/// <remarks>
/// The email will be used to register a new application user granting access to restricted area
/// </remarks>
public partial class Register(IMediator mediator) : Endpoint<RegisterDoctorRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post(RegisterDoctorRequest.Route);
        AllowAnonymous();
        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Doctor registered successfully");
            x.Response(StatusCodes.Status400BadRequest, "Invalid doctor data");
            x.Response(StatusCodes.Status409Conflict, "Doctor already registered");
            x.ExampleRequest = new RegisterDoctorRequest
            (
                "Dr. John Doe",
                "12345678909",
                "123456-RS",
                "doctor@email.com",
                "str0ngP@ssword"
            )
            { ConfirmPassword = "str0ngP@ssword" };
        });
    }

    public override async Task HandleAsync(RegisterDoctorRequest req, CancellationToken ct)
    {
        var command = new RegisterDoctorCommand(req.Name, req.Cpf, req.Crm, req.Email, req.Password, req.SpecialtyId);
        var result = await _mediator.Send(command, ct);

        await SendResultAsync(result.ToMinimalApiResult());
    }
}
