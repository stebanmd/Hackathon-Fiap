﻿using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.Appointments;
using Hackathon.Fiap.UseCases.Doctors.GetByUserId;
using Hackathon.Fiap.Web.Commons.Extensions;

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
        var doctor = await _mediator.Send(new GetDoctorByUserIdQuery(User.GetUserId()), ct);
        if (doctor is null)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var result = await _mediator.Send(new GetAppointmentsQuery(doctor.Id, req.Status), ct);
        Response = result.Select(x => new GetAppointmentsResponse(x.Id, x.Start, x.End, x.Status, x.PatientId));
    }
}
