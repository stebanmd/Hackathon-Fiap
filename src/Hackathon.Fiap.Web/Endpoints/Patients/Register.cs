using System.Net;
using Ardalis.Result.AspNetCore;
using Hackathon.Fiap.UseCases.Patients.Register;

namespace Hackathon.Fiap.Web.Endpoints.Patients;

/// <summary>
/// Register a new Patient
/// </summary>
/// <remarks>
/// Creates a new Patient with Application user.
/// </remarks>
public class Register(IMediator mediator) : Endpoint<RegisterPatientRequest>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post(RegisterPatientRequest.Route);
        AllowAnonymous();
        Summary(x =>
        {
            x.Response(StatusCodes.Status200OK, "Patient registered successfully.");
            x.Response(StatusCodes.Status400BadRequest, "Invalid Payload");
            x.Response(StatusCodes.Status409Conflict, "Patient already registered.");
            x.ExampleRequest = new RegisterPatientRequest
            (
                "Patient name",
                "12345678900",
                "patient@email.com",
                "str0ngP@ssword"
            )
            {  ConfirmPassord = "str0ngP@ssword" };
        });
    }

    public override async Task HandleAsync(RegisterPatientRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(
            new RegisterPatientCommand(req.Name, req.Cpf, req.Email, req.Password),
            ct);

        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
        }
        else
        {
            await SendResultAsync(result.ToMinimalApiResult());
        }
    }
}
