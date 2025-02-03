using FluentValidation;
using Hackathon.Fiap.Infrastructure.Data.Config;

namespace Hackathon.Fiap.Web.Endpoints.Doctors;

public class RegisterDoctorValidator : Validator<RegisterDoctorRequest>
{
    public RegisterDoctorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);

        RuleFor(x => x.Cpf)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_CPF_LENGTH)
            .Matches(@"^[\d]{11}$");

        RuleFor(x => x.Crm)
            .NotEmpty()
            .Matches(@"^[\d]{6}\/[A-Z]{2}$");

        RuleFor(x => x.Email)
            .NotEmpty()
            .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}
