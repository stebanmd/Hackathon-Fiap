using FluentAssertions;
using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.UseCases.Doctors.Specialties;

namespace Hackathon.Fiap.UnitTests.UseCases.Specialties;
public class GetSpecialtiesHandlerTests
{
    private readonly IRepository<Specialty> _repository = Substitute.For<IRepository<Specialty>>();
    private readonly GetSpecialtiesHandler _handler;

    public GetSpecialtiesHandlerTests()
    {
        _handler = new GetSpecialtiesHandler(_repository);
    }

    [Fact]
    public async Task ReturnsSuccess()
    {
        var specialties = new List<Specialty>
        {
            new Specialty { Id = 1, Name = "Cardiologia" },
            new Specialty { Id = 2, Name = "Ortopedia" }
        };

        _repository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(specialties));

        var result = await _handler.Handle(new GetSpecialtiesQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCanceled()
    {        
        Func<Task> act = async () => await _handler.Handle(new GetSpecialtiesQuery(), new CancellationToken(true));
        await act.Should().ThrowAsync<OperationCanceledException>();
    }

}
