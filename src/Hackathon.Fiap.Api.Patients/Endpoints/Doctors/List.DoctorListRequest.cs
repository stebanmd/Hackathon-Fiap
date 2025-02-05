namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

public record DoctorListRequest()
{
    public const string Route = "/doctors";

    public int? SpecialtyId { get; set; }
}
