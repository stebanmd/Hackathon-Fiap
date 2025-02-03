using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Patients;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddsPatientAndSetsId()
    {
        var patientFaker = new PatientFaker().Generate();
        var repository = GetRepository<Patient>();

        var patient = new Patient(patientFaker.Name, patientFaker.Cpf);
        patient.SetUser(patientFaker.User);

        await repository.AddAsync(patient);

        var newPatient = (await repository.ListAsync()).FirstOrDefault();

        Assert.Equal(patientFaker.Name, newPatient?.Name);
        Assert.Equal(patientFaker.Cpf, newPatient?.Cpf);
        Assert.True(newPatient?.Id > 0);
    }
}
