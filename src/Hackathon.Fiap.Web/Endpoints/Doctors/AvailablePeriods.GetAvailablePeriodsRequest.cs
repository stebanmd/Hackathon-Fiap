using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record GetAvailablePeriodsRequest(DateOnly Date)
{
    public const string Route = "doctors/{DoctorId:int}/availableTimes";

    [FromRoute]
    public int DoctorId { get; set; }
}
