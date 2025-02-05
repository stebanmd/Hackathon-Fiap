namespace Hackathon.Fiap.UseCases.Doctors.List;

public record ListDoctorsQuery(int? Skip, int? Take, int? SpecialtyId) : IQuery<Result<IEnumerable<DoctorDto>>>;
