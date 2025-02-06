namespace Hackathon.Fiap.Api.Patients.Endpoints.Patients;

public record RegisterPatientRequest(string Name, string Cpf, string Email, string Password)
{
    public const string Route = "/patients";
    public string? ConfirmPassword { get; set; }
}
