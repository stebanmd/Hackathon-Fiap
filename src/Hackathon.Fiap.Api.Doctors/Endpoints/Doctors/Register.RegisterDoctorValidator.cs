using Hackathon.Fiap.Api.Doctors.Commons.Validators;
using Hackathon.Fiap.Infrastructure.Data.Config;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Doctors;

public class RegisterDoctorValidator : Validator<RegisterDoctorRequest>
{
    public RegisterDoctorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        RuleFor(x => x.Cpf)
            .SetValidator(new CpfValidator(), "Cpf");

        RuleFor(x => x.Crm)
            .NotEmpty()
            .Matches(@"^[\d]{6}\-[A-Z]{2}$");

        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
            .WithMessage("Invalid email format.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);

        RuleFor(x => x.SpecialtyId)
            .GreaterThan(0)
            .When(id => id is not null);
    }

    public override Task<ValidationResult> ValidateAsync(FluentValidation.ValidationContext<RegisterDoctorRequest> context, CancellationToken cancellation = default)
    {
        // Validators are registered as Singleton, if we need to use a service scoped or transient,
        // we need to create a scope and resolve the service from there
        using var scope = CreateScope();
        var passwordValidator = scope.Resolve<PasswordValidator>();

        RuleFor(x => x.Password)
            .SetValidator(passwordValidator, "UserPassword");

        return base.ValidateAsync(context, cancellation);
    }
}
