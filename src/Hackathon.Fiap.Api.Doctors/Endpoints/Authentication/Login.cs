using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Authentication;

public class Login(SignInManager<ApplicationUser> signInManager) : Endpoint<ApplicationLoginRequest, AccessTokenResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public override void Configure()
    {
        Post("auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ApplicationLoginRequest req, CancellationToken ct)
    {
        _signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

        var result = await _signInManager.PasswordSignInAsync(req.Username, req.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            await SendUnauthorizedAsync(ct);
        }

        // The signInManager already produced the needed response in the form of bearer token.
    }
}
