using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Specifications;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.IntegrationTests.Fakers;

namespace Hackathon.Fiap.IntegrationTests.Data.Appointments;
public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Update()
    {
        var appointmentId = await AddAppointment();

        var repository = GetRepository<Appointment>();

        var spec = new GetAppointmentByIdSpec(appointmentId);

        var appointment = await repository.FirstOrDefaultAsync(spec);

        Assert.NotNull(appointment);

        appointment.Cancel("test");

        await repository.UpdateAsync(appointment);

        Assert.Equal(AppointmentStatus.Canceled, appointment.Status);
        Assert.Equal("test", appointment.Reason);
    }

    private async Task<int> AddAppointment()
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
        return appointment.Id;
    }
}
