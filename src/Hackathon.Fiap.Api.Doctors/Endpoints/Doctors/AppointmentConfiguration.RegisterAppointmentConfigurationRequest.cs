using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public record RegisterAppointmentConfigurationRequest(double Price, double Duration)
{
    public const string Route = "doctors/{Id:int}/configuration";

    [FromRoute]
    public int Id { get; set; }
}
