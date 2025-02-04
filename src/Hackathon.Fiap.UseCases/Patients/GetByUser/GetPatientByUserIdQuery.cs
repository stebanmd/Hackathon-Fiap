namespace Hackathon.Fiap.UseCases.Patients.GetByUser;
public record GetPatientByUserIdQuery(string UserId) : IQuery<PatientDto?>;
