namespace Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;

public class GetDoctorByIdSpec : Specification<Doctor>
{
    public GetDoctorByIdSpec(int doctorId)
    {
        Query
          .Include(doc => doc.User)
          .Include(doc => doc.Schedules)
          .Where(doc => doc.Id == doctorId);
    }
}
