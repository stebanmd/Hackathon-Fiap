namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record RegisterDoctorRequest(string Name, string Cpf, string Crm, string Email, string Password)
{
    public const string Route = "doctors/register";

    public string? ConfirmPassword { get; set; }
}
