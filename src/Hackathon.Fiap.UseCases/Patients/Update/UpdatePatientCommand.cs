namespace Hackathon.Fiap.UseCases.Patients.Update;
public record UpdatePatientCommand(int Id, string Name) : ICommand<Result>;
