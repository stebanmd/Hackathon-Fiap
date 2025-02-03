using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Patients;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task DeletesItemAfterAddingIt()
    {
        // add a Patient
        var patientFaker = new PatientFaker().Generate();
        var repository = GetRepository<Patient>();

        var patient = new Patient(patientFaker.Name, patientFaker.Cpf);
        patient.SetUser(patientFaker.User);

        await repository.AddAsync(patient);

        // delete the item
        await repository.DeleteAsync(patient);

        // verify it's no longer there
        Assert.DoesNotContain(await repository.ListAsync(), x => x.Name == patientFaker.Name);
    }
}
