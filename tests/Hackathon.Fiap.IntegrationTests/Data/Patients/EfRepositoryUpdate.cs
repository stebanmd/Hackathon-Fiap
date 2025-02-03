using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Patients;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task UpdatesItemAfterAddingIt()
    {
        // add a Patient
        var patientFaker = new PatientFaker().Generate();
        var repository = GetRepository<Patient>();

        var patient = new Patient(patientFaker.Name, patientFaker.Cpf);
        patient.SetUser(patientFaker.User);

        await repository.AddAsync(patient);

        // detach the item so we get a different instance
        _dbContext.Entry(patient).State = EntityState.Detached;

        // fetch the item and update its title
        var newPatient = (await repository.ListAsync()).FirstOrDefault(x => x.Name == patientFaker.Name);
        if (newPatient == null)
        {
            Assert.NotNull(newPatient);
            return;
        }
        Assert.NotSame(patient, newPatient);

        var newName = new PatientFaker().Generate().Name;
        newPatient.UpdateName(newName);

        // Update the item
        await repository.UpdateAsync(newPatient);

        // Fetch the updated item
        var updatedItem = (await repository.ListAsync())
            .FirstOrDefault(x => x.Name == newName);

        Assert.NotNull(updatedItem);
        Assert.NotEqual(patient.Name, updatedItem?.Name);
        Assert.Equal(patient.Cpf, updatedItem?.Cpf);
        Assert.Equal(newPatient.Id, updatedItem?.Id);
    }
}
