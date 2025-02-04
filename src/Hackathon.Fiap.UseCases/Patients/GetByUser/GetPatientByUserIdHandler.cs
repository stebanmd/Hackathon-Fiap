using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Aggregates.Patients.Specifications;

namespace Hackathon.Fiap.UseCases.Patients.GetByUser;

public class GetPatientByUserIdHandler(IRepository<Patient> repository) : IQueryHandler<GetPatientByUserIdQuery, PatientDto?>
{
    private readonly IRepository<Patient> _repository = repository;

    public async Task<PatientDto?> Handle(GetPatientByUserIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetPatientByUserIdSpec(request.UserId);
        var patient = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        return patient is null
            ? null
            : new PatientDto(patient.Id, patient.Name, patient.Cpf, patient.User.Email!);
    }
}
