using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Doctors;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddsDoctorAndSetsId()
    {
        var doctorFaker = new DoctorFaker().Generate();
        var repository = GetRepository<Doctor>();

        var doctor = new Doctor(doctorFaker.Name, doctorFaker.Cpf, doctorFaker.Crm);
        doctor.SetUser(doctorFaker.User);
        foreach (var schedule in doctorFaker.Schedules)
        {
            doctor.AddSchedule(schedule);
        }

        await repository.AddAsync(doctor);

        var newDoctor = (await repository.ListAsync()).FirstOrDefault();

        Assert.Equal(doctorFaker.Name, newDoctor?.Name);
        Assert.Equal(doctorFaker.Cpf, newDoctor?.Cpf);
        Assert.True(newDoctor?.Id > 0);
    }
}
