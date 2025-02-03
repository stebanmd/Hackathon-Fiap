using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Infrastructure.Authorization;

public record PatientMustBeDataOwnerRequirement(int PatientId) : IAuthorizationRequirement;
