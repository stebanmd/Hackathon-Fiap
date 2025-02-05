namespace Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;
public record GetAvailablePeriodsQuery(int DoctorId, DateOnly Date) : IQuery<Result<AvailableAppointmentsDto>>;
