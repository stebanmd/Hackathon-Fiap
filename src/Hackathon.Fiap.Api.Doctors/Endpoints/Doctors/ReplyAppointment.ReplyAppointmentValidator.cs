namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public class ReplyAppointmentValidator : Validator<ReplyAppointmentRequest>
{
    public ReplyAppointmentValidator()
    {
        RuleFor(x => x.AppointmentId).GreaterThan(0);
        RuleFor(x => x.Reason)
            .MinimumLength(10)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Reason));
        RuleFor(x => x.Status).IsInEnum();
    }
}
