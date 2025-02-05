using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

public record GetAvailablePeriodsRequest(DateOnly Date)
{
    public const string Route = "doctors/{DoctorId:int}/periods";

    [FromRoute]
    public int DoctorId { get; set; }
}
