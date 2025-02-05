using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.UseCases.Doctors.Specialties;

public class GetSpecialtiesHandler(IRepository<Specialty> repository) : IQueryHandler<GetSpecialtiesQuery, IEnumerable<SpecialtyDto>>
{
    private readonly IRepository<Specialty> _repository = repository;

    public async Task<IEnumerable<SpecialtyDto>> Handle(GetSpecialtiesQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await _repository.ListAsync(cancellationToken);

        return result.Select(x => new SpecialtyDto(x.Id, x.Name));
    }
}
