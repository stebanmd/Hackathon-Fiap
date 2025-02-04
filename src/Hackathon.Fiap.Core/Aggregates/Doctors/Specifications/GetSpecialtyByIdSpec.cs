namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetSpecialtyByIdSpec : Specification<Specialty>
{
    public GetSpecialtyByIdSpec(int id)
    {
        Query
            .Where(x => x.Id == id);
    }
}
