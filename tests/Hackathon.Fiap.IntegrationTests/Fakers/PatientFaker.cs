using Bogus;
using Bogus.Extensions.Brazil;
using Hackathon.Fiap.Core.Aggregates.Patients;

namespace Hackathon.Fiap.IntegrationTests.Fakers;
public class PatientFaker : Faker<Patient>
{
    public PatientFaker()
    {
        var userId = Guid.NewGuid().ToString();

        RuleFor(x => x.Name, x => x.Person.FirstName);
        RuleFor(x => x.Cpf, x => x.Person.Cpf());
        RuleFor(x => x.UserId, userId);
        RuleFor(x => x.User, x => new()
        {
            Id = userId,
            UserName = x.Person.UserName,
            Email = x.Person.Email,
            EmailConfirmed = true
        });
    }
}
