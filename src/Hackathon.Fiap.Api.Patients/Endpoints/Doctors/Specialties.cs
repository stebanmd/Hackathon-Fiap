using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.Specialties;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

/// <summary>
/// List available specialties
/// </summary>
public partial class Specialties(IMediator mediator) : EndpointWithoutRequest<IEnumerable<GetSpecialtiesResponse>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("doctors/specialties");
        Roles(ApplicationRoles.Patient, ApplicationRoles.Admin);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSpecialtiesQuery(), ct);
        Response = result.Select(x => new GetSpecialtiesResponse(x.Id, x.Name));
    }
}
