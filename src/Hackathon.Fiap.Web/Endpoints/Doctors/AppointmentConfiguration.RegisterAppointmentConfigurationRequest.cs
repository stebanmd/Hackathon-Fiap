using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record RegisterAppointmentConfigurationRequest(double Price, double Duration)
{
    public const string Route = "doctors/{Id:int}/appointmentConfiguration";

    [FromRoute]
    public int Id { get; set; }
}
