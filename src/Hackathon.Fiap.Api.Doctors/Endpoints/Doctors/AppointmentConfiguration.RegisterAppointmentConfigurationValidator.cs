namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public class RegisterAppointmentConfigurationValidator : Validator<RegisterAppointmentConfigurationRequest>
{
    public RegisterAppointmentConfigurationValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Duration)
            .Must(x => x is 30 or 60);
    }
}
