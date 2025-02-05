using Hackathon.Fiap.Api.Doctors.Endpoints.Contributors;
using Hackathon.Fiap.Infrastructure.Data;

namespace Hackathon.Fiap.FunctionalTests.ApiEndpoints.Contributors;

[Collection("Sequential")]
public class List(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task ReturnsTwoContributors()
    {
        var result = await _client.GetAndDeserializeAsync<ContributorListResponse>("/Contributors");

        Assert.Equal(2, result.Contributors.Count);
        Assert.Contains(result.Contributors, i => i.Id == SeedData.Contributor1.Id);
        Assert.Contains(result.Contributors, i => i.Id == SeedData.Contributor2.Id);
    }
}
