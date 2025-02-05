namespace Hackathon.Fiap.Web.Endpoints.Appointments;

public sealed class CreateAppointmentValidator : Validator<CreateAppointmentRequest>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.Start).GreaterThan(DateTime.Now);
    }
}
