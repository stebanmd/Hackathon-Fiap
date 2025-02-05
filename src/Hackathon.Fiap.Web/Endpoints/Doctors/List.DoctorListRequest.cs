namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record DoctorListRequest()
{
    public const string Route = "/doctors";

    public int? SpecialtyId { get; set; }
}
