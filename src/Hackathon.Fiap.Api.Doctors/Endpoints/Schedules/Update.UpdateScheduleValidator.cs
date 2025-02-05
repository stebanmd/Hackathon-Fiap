namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

public class UpdateScheduleValidator : Validator<UpdateScheduleRequest>
{
    public UpdateScheduleValidator()
    {
        RuleFor(x => x.ScheduleId).NotEmpty();
        RuleFor(x => x.DayOfWeek).IsInEnum();

        RuleFor(x => x.Day)
            .NotEmpty()
            .When(x => x.DayOfWeek is null);

        RuleFor(x => x.Day)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.Day is not null);

        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End).NotEmpty().GreaterThan(x => x.Start);
    }
}
