namespace Hackathon.Fiap.Core.Aggregates.Patients.Specifications;

public class GetPatientByUserIdSpec : Specification<Patient>
{
    public GetPatientByUserIdSpec(string userId)
    {
        Query
            .Include(patient => patient.User)
            .Where(patient => patient.UserId == userId);
    }
}
