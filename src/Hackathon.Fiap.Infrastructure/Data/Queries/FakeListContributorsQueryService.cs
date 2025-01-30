using Hackathon.Fiap.UseCases.Contributors;
using Hackathon.Fiap.UseCases.Contributors.List;

namespace Hackathon.Fiap.Infrastructure.Data.Queries;

public class FakeListContributorsQueryService : IListContributorsQueryService
{
    public Task<IEnumerable<ContributorDto>> ListAsync()
    {
        IEnumerable<ContributorDto> result =
        [
            new ContributorDto(1, "Fake Contributor 1", ""),
            new ContributorDto(2, "Fake Contributor 2", "")
        ];

        return Task.FromResult(result);
    }
}
