using Hackathon.Fiap.Core.Aggregates.Patients;
using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Patients.Register;

public sealed class RegisterPatientHandler(
    ILogger<RegisterPatientHandler> logger,
    IRepository<Patient> repository,
    UserManager<ApplicationUser> userManager,
    IUserStore<ApplicationUser> userStore) : ICommandHandler<RegisterPatientCommand, Result<int>>
{
    private readonly ILogger<RegisterPatientHandler> _logger = logger;
    private readonly IRepository<Patient> _repository = repository;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUserStore<ApplicationUser> _userStore = userStore;

    public async Task<Result<int>> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            _logger.LogError("Email {Email} already in use.", request.Email);
            return Result<int>.Conflict("User already exists");
        }

        user = new();
        var emailStore = (IUserEmailStore<ApplicationUser>)_userStore;

        await _userStore.SetUserNameAsync(user, request.Email, cancellationToken);
        await emailStore.SetEmailAsync(user, request.Email, cancellationToken);

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            _logger.LogError("Error creating user: {0}", identityResult.Errors);
            return Result<int>.Error("Error creating user");
        }

        await _userManager.AddToRoleAsync(user, ApplicationRoles.Patient);

        var newPatient = new Patient(request.Name, request.Cpf);
        newPatient.SetUser(user);

        var createdItem = await _repository.AddAsync(newPatient, cancellationToken);
        return createdItem.Id;
    }
}
