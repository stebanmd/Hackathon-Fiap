namespace Hackathon.Fiap.UseCases.Doctors.GetByUserId;

public record GetDoctorByUserIdQuery(string UserId) : IQuery<DoctorDto?>;
