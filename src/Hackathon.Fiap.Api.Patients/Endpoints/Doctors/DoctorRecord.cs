namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

public record DoctorRecord(int Id, string Name, string Cpf, string Crm, int? SpecialtyId);
