using Microsoft.AspNetCore.Identity;

namespace Hackathon.Fiap.Core.Aggregates.Users;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
