using Hackathon.Fiap.Core.Aggregates.Appointments.Events;

namespace Hackathon.Fiap.Core.Aggregates.Appointments;
public class Appointment(DateTime start, DateTime end, int patientId, int doctorId) : EntityBase, IAggregateRoot
{
    public DateTime Start { get; private set; } = start;
    public DateTime End { get; private set; } = end;

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Confirmed;

    public int PatientId { get; private set; } = patientId;
    public Patients.Patient Patient { get; set; } = default!;

    public int DoctorId { get; private set; } = doctorId;
    public Doctors.Doctor Doctor { get; set; } = default!;

    public void Cancel()
    {
        Status = AppointmentStatus.Canceled;
        RegisterDomainEvent(new CancelAppointmentEvent(this));
    }

    public void Confirm()
    {
        Status = AppointmentStatus.Confirmed;
        //RegisterDomainEvent(new ConfirmAppointmentEvent(this));
    }
}
