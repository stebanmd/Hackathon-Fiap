using FluentValidation;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Schedules;

public class CreateScheduleValidator : Validator<CreateScheduleRequest>
{
    public CreateScheduleValidator()
    {
        RuleFor(x => x.DaysOfWeek)
            .Empty()
            .When(x => x.Day is not null)
            .WithMessage("'DaysOfWeek' should be empty when informing a specific day.");

        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).NotEmpty().GreaterThan(x => x.StartTime);

        RuleFor(x => x.Day)
            .NotEmpty()
            .When(x => x.DaysOfWeek.Count == 0);
    }
}
