using System.Security.Claims;
using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Hackathon.Fiap.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Infrastructure.Authorization;

public class DoctorMustBeDataOwnerHandler : AuthorizationHandler<DoctorMustBeDataOwnerRequirement>
{
    private readonly ILogger<DoctorMustBeDataOwnerHandler> _logger;
    private readonly IRepository<Doctor> _repository;

    public DoctorMustBeDataOwnerHandler(
        ILogger<DoctorMustBeDataOwnerHandler> logger,
        IRepository<Doctor> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DoctorMustBeDataOwnerRequirement requirement)
    {
        if (context.User is null)
        {
            context.Fail();
            return;
        }

        var role = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);
        if (role.Contains(ApplicationRoles.Admin))
        {
            _logger.LogInformation("Access granted to Admin user.");
            context.Succeed(requirement);
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            _logger.LogError("UserID not found on user claims.");
            context.Fail();
            return;
        }

        var spec = new GetDoctorByUserIdSpec(userId);
        var doctor = await _repository.FirstOrDefaultAsync(spec);

        if (doctor is null)
        {
            _logger.LogWarning("Doctor not found.");
            context.Fail();
            return;
        }

        if (doctor.Id != requirement.DoctorId)
        {
            _logger.LogWarning("Doctor is not the data owner.");
            context.Fail();
        }

        context.Succeed(requirement);
    }
}
