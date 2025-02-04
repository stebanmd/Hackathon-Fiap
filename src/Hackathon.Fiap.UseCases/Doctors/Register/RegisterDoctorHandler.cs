using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Doctors.Specifications;
using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Hackathon.Fiap.UseCases.Doctors.Register;

public class RegisterDoctorHandler(
    ILogger<RegisterDoctorHandler> logger,
    IRepository<Doctor> doctorsRepository,
    IRepository<Specialty> specialtyRepository,
    UserManager<ApplicationUser> userManager,
    IUserStore<ApplicationUser> userStore) : ICommandHandler<RegisterDoctorCommand, Result<int>>
{
    private readonly ILogger<RegisterDoctorHandler> _logger = logger;
    private readonly IRepository<Doctor> _doctorsRepository = doctorsRepository;
    private readonly IRepository<Specialty> _specialtyRepository = specialtyRepository;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUserStore<ApplicationUser> _userStore = userStore;

    public async Task<Result<int>> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Registering doctor {Name} with email {Email}", request.Name, request.Email);

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

        await _userManager.AddToRoleAsync(user, ApplicationRoles.Doctor);

        var newDoctor = new Doctor(request.Name, request.Cpf, request.Crm);
        newDoctor.SetUser(user);

        if (request.specialtyId.HasValue)
        {
            var specialty = await _specialtyRepository.FirstOrDefaultAsync(new GetSpecialtyByIdSpec(request.specialtyId.Value), cancellationToken);
            newDoctor.SetSpecialty(specialty);
        }

        var createdItem = await _doctorsRepository.AddAsync(newDoctor, cancellationToken);
        return createdItem.Id;
    }
}
