using Hackathon.Fiap.UseCases;
using Hackathon.Fiap.UseCases.Doctors.List;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

/// <summary>
/// List all Doctors
/// </summary>
/// <remarks>
/// List all doctors - returns a DoctorListResponse containing the Doctors.
/// </remarks>
public class List(IMediator _mediator) : Endpoint<DoctorListRequest, DoctorListResponse>
{
    public override void Configure()
    {
        Get(DoctorListRequest.Route);
        Roles(ApplicationRoles.Patient, ApplicationRoles.Admin);
    }

    public override async Task HandleAsync(DoctorListRequest req, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var result = await _mediator.Send(new ListDoctorsQuery(null, null, req.SpecialtyId), ct);

        if (result.IsSuccess)
        {
            Response = new DoctorListResponse
            {
                Doctors = result.Value.Select(c => new DoctorRecord(c.Id, c.Name, c.Cpf, c.Crm, c.SpecialtyId)).ToList()
            };
        }
    }
}
