namespace Hackathon.Fiap.Api.Patients.Endpoints.Appointments;

public class CancelAppointmentValidator : Validator<CancelAppointmentRequest>
{
    public CancelAppointmentValidator()
    {
        RuleFor(x => x.AppointmentId).GreaterThan(0);
        RuleFor(x => x.Reason)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(500);
    }
}
