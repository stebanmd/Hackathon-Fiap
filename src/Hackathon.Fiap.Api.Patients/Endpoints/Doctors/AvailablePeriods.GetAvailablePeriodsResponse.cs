namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

public record GetAvailablePeriodsResponse(double Price, IEnumerable<TimeOnly> AvailableTimes);
