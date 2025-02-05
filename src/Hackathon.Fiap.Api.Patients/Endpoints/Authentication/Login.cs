using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Api.Patients.Endpoints.Authentication;

public class Login(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Endpoint<ApplicationLoginRequest, AccessTokenResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override void Configure()
    {
        Post("auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ApplicationLoginRequest req, CancellationToken ct)
    {
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

        ApplicationUser? user;
        if (req.Username.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(req.Username);
        }
        else
        {
            user = await _userManager.FindByNameAsync(req.Username);
        }

        if (user is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var result = await _signInManager.PasswordSignInAsync(user, req.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            await SendUnauthorizedAsync(ct);
        }

        // The signInManager already produced the needed response in the form of bearer token.
    }
}
