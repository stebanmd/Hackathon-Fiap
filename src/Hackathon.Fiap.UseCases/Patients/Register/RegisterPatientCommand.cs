namespace Hackathon.Fiap.UseCases.Patients.Register;

public record RegisterPatientCommand(string Name, string Cpf, string Email,string Password) : ICommand<Result<int>>;
