using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

namespace Hackathon.Fiap.UseCases.Doctors.GetByUserId;

public class GetDoctorByUserIdHandler(IRepository<Doctor> repository) : IQueryHandler<GetDoctorByUserIdQuery, DoctorDto?>
{
    private readonly IRepository<Doctor> _repository = repository;

    public async Task<DoctorDto?> Handle(GetDoctorByUserIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetDoctorByUserIdSpec(request.UserId);
        var doctor = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        return doctor is null
            ? null
            : new DoctorDto(doctor.Id, doctor.Name, doctor.Cpf, doctor.Crm);
    }
}

