using Hackathon.Fiap.Core.Aggregates.Patients;

namespace Hackathon.Fiap.UseCases.Patients.Update;
internal class UpdatePatientHandler(IRepository<Patient> repository) : ICommandHandler<UpdatePatientCommand, Result>
{
    private readonly IRepository<Patient> _repository = repository;

    public async Task<Result> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (patient is null)
        {
            return Result.NotFound();
        }

        patient.UpdateName(request.Name);

        await _repository.UpdateAsync(patient, cancellationToken);
        return Result.Success();
    }
}
