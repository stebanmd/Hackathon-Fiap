namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetDoctorByUserIdSpec : Specification<Doctor>
{
    public GetDoctorByUserIdSpec(string userId)
    {
        Query
          .Include(doc => doc.User)
          .Where(doc => doc.UserId == userId);
    }
}
