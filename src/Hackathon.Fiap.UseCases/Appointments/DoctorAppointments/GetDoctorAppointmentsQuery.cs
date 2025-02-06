using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Appointments.DoctorAppointments;
public record class GetDoctorAppointmentsQuery(int DoctorId, AppointmentStatus? Status) : IQuery<IEnumerable<AppointmentsDto>>;
