using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public record GetAppointmentsResponse(int Id, DateTime Start, DateTime End, AppointmentStatus Status, int PatientId);
