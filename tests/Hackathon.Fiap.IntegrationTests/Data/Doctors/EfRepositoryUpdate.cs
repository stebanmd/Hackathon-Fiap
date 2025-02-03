using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Doctors;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task UpdatesItemAfterAddingIt()
    {
        // add a Doctor
        var doctorFaker = new DoctorFaker().Generate();
        var repository = GetRepository<Doctor>();

        var doctor = new Doctor(doctorFaker.Name, doctorFaker.Cpf, doctorFaker.Crm);
        doctor.SetUser(doctorFaker.User);
        foreach (var schedule in doctorFaker.Schedules)
        {
            doctor.AddSchedule(schedule);
        }

        await repository.AddAsync(doctor);

        // detach the item so we get a different instance
        _dbContext.Entry(doctor).State = EntityState.Detached;

        // fetch the item and update its title
        var newDoctor = (await repository.ListAsync()).FirstOrDefault(x => x.Name == doctorFaker.Name);
        if (newDoctor == null)
        {
            Assert.NotNull(newDoctor);
            return;
        }
        Assert.NotSame(doctor, newDoctor);

        var newName = new DoctorFaker().Generate().Name;
        newDoctor.UpdateName(newName);

        // Update the item
        await repository.UpdateAsync(newDoctor);

        // Fetch the updated item
        var updatedItem = (await repository.ListAsync())
            .FirstOrDefault(x => x.Name == newName);

        Assert.NotNull(updatedItem);
        Assert.NotEqual(doctor.Name, updatedItem?.Name);
        Assert.Equal(doctor.Cpf, updatedItem?.Cpf);
        Assert.Equal(newDoctor.Id, updatedItem?.Id);
    }
}
