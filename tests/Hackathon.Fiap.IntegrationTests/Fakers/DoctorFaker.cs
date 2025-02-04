using AutoBogus;
using Bogus.Extensions.Brazil;
using Hackathon.Fiap.Core.Aggregates.Doctors;

namespace Hackathon.Fiap.IntegrationTests.Fakers;
public class DoctorFaker : AutoFaker<Doctor>
{
    public DoctorFaker()
    {
        var userId = Guid.NewGuid().ToString();

        RuleFor(x => x.Name, x => x.Person.FirstName);
        RuleFor(x => x.Cpf, x => x.Person.Cpf());
        RuleFor(x => x.Crm, x => x.Random.Number(1, 10).ToString());
        RuleFor(x => x.UserId, userId);
        RuleFor(x => x.User, x => new()
        {
            Id = userId,
            UserName = x.Person.UserName,
            Email = x.Person.Email,
            EmailConfirmed = true
        });
        RuleFor(x => x.Schedules, x =>
        [
            new()
            {
                DayOfWeek = DayOfWeek.Monday,
                Start = new TimeOnly(8, 0),
                End = new TimeOnly(9, 0)
            },
            new()
            {
                Day = new DateOnly(2025, 1, 1),
                Start = new TimeOnly(10, 0),
                End = new TimeOnly(11, 0)
            },
        ]);
    }
}
