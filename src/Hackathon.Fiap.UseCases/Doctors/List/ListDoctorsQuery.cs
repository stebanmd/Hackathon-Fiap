namespace Hackathon.Fiap.UseCases.Doctors.List;

public record ListDoctorsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<DoctorDto>>>;
