using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.Appointments;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

/// <summary>
/// List available appointments
/// </summary>
public partial class Appointments(IMediator mediator) : Endpoint<GetAppointmentsRequest, IEnumerable<GetAppointmentsResponse>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get(GetAppointmentsRequest.Route);
        Roles(ApplicationRoles.Doctor, ApplicationRoles.Admin);
    }

    public override async Task HandleAsync(GetAppointmentsRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAppointmentsQuery(req.Status), ct);
        Response = result.Select(x => new GetAppointmentsResponse(x.Id, x.Start, x.End, x.Status, x.PatientId));
    }
}
