using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Appointments.Appointments;
public record class GetAppointmentsQuery(int DoctorId, AppointmentStatus? Status) : IQuery<IEnumerable<AppointmentsDto>>;
