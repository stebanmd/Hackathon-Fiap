using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Appointments.PatientAppointments;
public record class GetPatientAppointmentsQuery(int PatientId, AppointmentStatus? Status) : IQuery<IEnumerable<AppointmentsDto>>;
