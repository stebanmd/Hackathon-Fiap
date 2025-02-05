namespace Hackathon.Fiap.UseCases.Doctors.AvailablePeriod;

public record AvailableAppointmentsDto(double Price, IEnumerable<TimeOnly> AvailableTimes);
