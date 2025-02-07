using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Core.Aggregates.Users;

public class ApplicationRoles : IdentityRole
{
    public const string Patient = "Patient";
    public const string Doctor = "Doctor";
}
