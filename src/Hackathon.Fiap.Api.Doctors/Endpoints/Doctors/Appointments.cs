using Hackathon.Fiap.UseCases.Appointments.DoctorAppointments;
using Hackathon.Fiap.UseCases.Doctors.GetByUserId;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

/// <summary>
/// List available appointments
/// </summary>
public partial class Appointments(IMediator mediator) : Endpoint<GetAppointmentsRequest, IEnumerable<GetAppointmentsResponse>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get(GetAppointmentsRequest.Route);
        Roles(ApplicationRoles.Doctor);
    }

    public override async Task HandleAsync(GetAppointmentsRequest req, CancellationToken ct)
    {
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var result = await _mediator.Send(new GetDoctorAppointmentsQuery(doctor.Id, req.Status), ct);
        Response = result.Select(x => new GetAppointmentsResponse(x.Id, x.Start, x.End, x.Status, x.PatientId, x.PatientName));
    }
}
