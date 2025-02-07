using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Appointments;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Add()
    {
        var start = new DateTime(2025, 3, 3, 8, 0, 0, DateTimeKind.Unspecified);
        var end = new DateTime(2025, 3, 3, 9, 0, 0, DateTimeKind.Unspecified);

        var appointment = new Appointment(start, end);

        var doctorFaker = new DoctorFaker().Generate();
        var doctor = new Doctor(doctorFaker.Name, doctorFaker.Cpf, doctorFaker.Crm);
        doctor.SetUser(doctorFaker.User);

        var patientFaker = new PatientFaker().Generate();
        var patient = new Patient(patientFaker.Name, patientFaker.Cpf);
        patient.SetUser(patientFaker.User);

        appointment.SetPatient(patient);
        appointment.SetDoctor(doctor);

        var repository = GetRepository<Appointment>();

        await repository.AddAsync(appointment);

        Assert.Equal(appointment.Start, start);
        Assert.Equal(appointment.End, end);
        Assert.True(appointment.Id > 0);
        Assert.Equal(AppointmentStatus.Pending, appointment.Status);

    }
}
