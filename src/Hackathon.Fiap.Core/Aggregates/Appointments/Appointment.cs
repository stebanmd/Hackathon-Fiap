using Hackathon.Fiap.Core.Aggregates.Appointments.Events;

namespace Hackathon.Fiap.Core.Aggregates.Appointments;

public class Appointment(DateTime start, DateTime end) : EntityBase, IAggregateRoot
{
    public DateTime Start { get; private set; } = start;
    public DateTime End { get; private set; } = end;

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

    public int PatientId { get; private set; }
    public Patients.Patient Patient { get; set; } = default!;

    public int DoctorId { get; private set; }
    public Doctors.Doctor Doctor { get; private set; } = default!;

    public string? Reason { get; private set; }

    public void SetDoctor(Doctors.Doctor doctor)
    {
        Doctor = doctor;
        DoctorId = doctor.Id;
    }

    public void SetPatient(Patients.Patient patient)
    {
        Patient = patient;
        PatientId = patient.Id;
    }

    public void Cancel(string? reason)
    {
        Status = AppointmentStatus.Canceled;
        Reason = reason;
        RegisterDomainEvent(new CancelAppointmentEvent(this));
    }

    public void Reply(AppointmentStatus status, string? reason)
    {
        Status = status;
        Reason = reason;
        RegisterDomainEvent(new ReplyAppointmentEvent(this));
    }
}
