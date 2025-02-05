using Hackathon.Fiap.Core.Aggregates.Appointments;
using Hackathon.Fiap.Core.Aggregates.Appointments.Events;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Interfaces;

namespace Hackathon.Fiap.Core.Services;

public class CreateAppointmentService(IRepository<Appointment> _repository, IMediator _mediator) : ICreateAppointmentService
{
    public async Task<int> Create(Patient patient, Doctor doctor, DateTime start, DateTime end)
    {
        var appointment = new Appointment(start, end);

        appointment.SetPatient(patient);
        appointment.SetDoctor(doctor);

        await _repository.AddAsync(appointment);

        var domainEvent = new CreateAppointmentEvent(appointment);
        await _mediator.Publish(domainEvent);

        return appointment.Id;
    }
}
