using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Doctors;

public record AppointmentsDto(int Id, DateTime Start, DateTime End, AppointmentStatus Status, int PatientId);
