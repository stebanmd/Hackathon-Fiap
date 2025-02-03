namespace Hackathon.Fiap.UseCases.Doctors.Register;

public record RegisterDoctorCommand(string Name, string Cpf, string Crm, string Email, string Password) : ICommand<Result<int>>;
