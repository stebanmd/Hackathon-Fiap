using Hackathon.Fiap.UseCases.Doctors.Specilaties;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

/// <summary>
/// List available specialties
/// </summary>
public partial class Specialties(IMediator mediator) : EndpointWithoutRequest<IEnumerable<GetSpecialtiesRespose>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("doctors/specialties");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSpecialtiesQuery(), ct);
        Response = result.Select(x => new GetSpecialtiesRespose(x.Id, x.Name));
    }
}
