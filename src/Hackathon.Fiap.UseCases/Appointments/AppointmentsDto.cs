using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Appointments;

public record AppointmentsDto(int Id, DateTime Start, DateTime End, AppointmentStatus Status, int PatientId, string PatientName, int DoctorId, string DoctorName);
