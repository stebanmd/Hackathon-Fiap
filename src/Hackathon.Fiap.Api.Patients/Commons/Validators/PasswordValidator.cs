using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Hackathon.Fiap.Api.Patients.Commons.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public PasswordValidator(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> identityOptions)
    {
        _userManager = userManager;

        RuleSet("UserPassword", () =>
        {
            // Applies all password rules from UserManager
            RuleFor(x => x).MustAsync(ValidatePasswordAsync);
        });
    }

    private async Task<bool> ValidatePasswordAsync(string val, string password, FluentValidation.ValidationContext<string> context, CancellationToken cancellationToken)
    {
        foreach (var validator in _userManager.PasswordValidators)
        {
            var result = await validator.ValidateAsync(_userManager, null, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    context.AddFailure(error.Description);
                }
            }
        }
        return true;
    }
}
