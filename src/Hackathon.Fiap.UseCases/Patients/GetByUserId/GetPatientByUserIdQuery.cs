namespace Hackathon.Fiap.UseCases.Patients.GetByUserId;

public record GetPatientByUserIdQuery(string UserId) : IQuery<PatientDto?>;
