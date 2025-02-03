using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Doctors;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task DeletesItemAfterAddingIt()
    {
        // add a Patient
        var doctorFaker = new DoctorFaker().Generate();
        var repository = GetRepository<Doctor>();

        var doctor = new Doctor(doctorFaker.Name, doctorFaker.Cpf, doctorFaker.Crm);
        doctor.SetUser(doctorFaker.User);
        foreach (var schedule in doctorFaker.Schedules)
        {
            doctor.AddSchedule(schedule);
        }

        await repository.AddAsync(doctor);

        // delete the item
        await repository.DeleteAsync(doctor);

        // verify it's no longer there
        Assert.DoesNotContain(await repository.ListAsync(), x => x.Name == doctorFaker.Name);
    }
}
