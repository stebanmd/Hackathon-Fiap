using Hackathon.Fiap.Core.Aggregates.Appointments;

namespace Hackathon.Fiap.UseCases.Doctors.Appointments;
public record class GetAppointmentsQuery(AppointmentStatus? Status) : IQuery<IEnumerable<AppointmentsDto>>;
