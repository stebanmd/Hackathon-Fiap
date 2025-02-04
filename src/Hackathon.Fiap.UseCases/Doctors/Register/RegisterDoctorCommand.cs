namespace Hackathon.Fiap.UseCases.Doctors.Register;

public record RegisterDoctorCommand(string Name, string Cpf, string Crm, string Email, string Password, int? specialtyId) : ICommand<Result<int>>;
