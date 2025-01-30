namespace Hackathon.Fiap.UseCases.Contributors.List;

public class ListContributorsHandler(IListContributorsQueryService _query)
  : IQueryHandler<ListContributorsQuery, Result<IEnumerable<ContributorDto>>>
{
    public async Task<Result<IEnumerable<ContributorDto>>> Handle(ListContributorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();

        return Result.Success(result);
    }
}
