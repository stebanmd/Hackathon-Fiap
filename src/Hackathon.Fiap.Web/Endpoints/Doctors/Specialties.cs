using Hackathon.Fiap.UseCases.Doctors.Specilaties;
using static Hackathon.Fiap.Web.Endpoints.Doctors.Specialties;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

/// <summary>
/// List available specialties
/// </summary>
public class Specialties(IMediator mediator) : EndpointWithoutRequest<IEnumerable<GetSpecialtiesRespose>>
{
    private readonly IMediator _mediator = mediator;

    public record GetSpecialtiesRespose(int Id, string Name);

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
