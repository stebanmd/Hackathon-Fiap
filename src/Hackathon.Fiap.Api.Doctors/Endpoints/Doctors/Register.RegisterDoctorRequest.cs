namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public record RegisterDoctorRequest(string Name, string Cpf, string Crm, string Email, string Password)
{
    public const string Route = "doctors";

    public string? ConfirmPassword { get; set; }

    public int? SpecialtyId { get; set; }
}
