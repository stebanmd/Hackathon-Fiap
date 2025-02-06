using Hackathon.Fiap.Api.Patients.Commons.Extensions;
using Hackathon.Fiap.UseCases.Appointments.PatientAppointments;
using Hackathon.Fiap.UseCases.Patients.GetByUserId;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

/// <summary>
/// List patient appointments
/// </summary>
public partial class List(IMediator mediator) : Endpoint<GetAppointmentsRequest, IEnumerable<GetAppointmentsResponse>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get(GetAppointmentsRequest.Route);
        Roles(ApplicationRoles.Patient);
    }

    public override async Task HandleAsync(GetAppointmentsRequest req, CancellationToken ct)
    {
        var patient = await _mediator.Send(new GetPatientByUserIdQuery(User.GetUserId()), ct);
        if (patient is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var result = await _mediator.Send(new GetPatientAppointmentsQuery(patient.Id, req.Status), ct);
        Response = result.Select(x => new GetAppointmentsResponse(x.Id, x.Start, x.End, x.Status, x.PatientId, x.PatientName, x.DoctorId, x.DoctorName));
    }
}
