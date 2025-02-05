namespace Hackathon.Fiap.Api.Patients.Endpoints.Doctors;

public class GetAvailablePeriodsValidator : Validator<GetAvailablePeriodsRequest>
{
    public GetAvailablePeriodsValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .GreaterThan(DateOnly.FromDateTime(DateTime.Today));
    }
}
