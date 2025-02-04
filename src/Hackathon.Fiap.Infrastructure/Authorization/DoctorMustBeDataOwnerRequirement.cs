using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Infrastructure.Authorization;

public record DoctorMustBeDataOwnerRequirement(int DoctorId) : IAuthorizationRequirement;
