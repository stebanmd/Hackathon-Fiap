using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Patients;

public record GetAppointmentsResponse(int Id, DateTime Start, DateTime End, AppointmentStatus Status, int PatientId, string PatientName, int DoctorId, string DoctorName);
