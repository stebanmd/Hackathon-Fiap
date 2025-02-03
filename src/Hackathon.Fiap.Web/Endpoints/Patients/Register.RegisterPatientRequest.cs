namespace Hackathon.Fiap.Web.Endpoints.Patients;

public record RegisterPatientRequest(string Name, string Cpf, string Email, string Password)
{
    public const string Route = "/patients/register";
    public string? ConfirmPassword { get; set; }
}
