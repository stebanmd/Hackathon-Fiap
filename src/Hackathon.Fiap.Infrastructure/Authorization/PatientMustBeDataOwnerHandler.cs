using System.Security.Claims;
using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Aggregates.Patients.Specifications;
using Microsoft.AspNetCore.Authorization;

namespace Hackathon.Fiap.Infrastructure.Authorization;

public class PatientMustBeDataOwnerHandler : AuthorizationHandler<PatientMustBeDataOwnerRequirement>
{
    private readonly ILogger<PatientMustBeDataOwnerHandler> _logger;
    private readonly IRepository<Patient> _repository;

    public PatientMustBeDataOwnerHandler(
        ILogger<PatientMustBeDataOwnerHandler> logger,
        IRepository<Patient> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PatientMustBeDataOwnerRequirement requirement)
    {
        if (context.User is null)
        {
            context.Fail();
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            _logger.LogError("UserID not found on user claims.");
            context.Fail();
            return;
        }

        var spec = new GetPatientByUserIdSpec(userId);
        var patient = await _repository.FirstOrDefaultAsync(spec);

        if (patient is null)
        {
            _logger.LogWarning("Patient not found.");
            context.Fail();
            return;
        }

        if (patient.Id != requirement.PatientId)
        {
            _logger.LogWarning("Patient is not the data owner.");
            context.Fail();
        }

        context.Succeed(requirement);
    }
}
