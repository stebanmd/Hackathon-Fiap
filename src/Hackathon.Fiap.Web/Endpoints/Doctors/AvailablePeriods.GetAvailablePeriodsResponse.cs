namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public record GetAvailablePeriodsResponse(double Price, IEnumerable<TimeOnly> AvailableTimes);
