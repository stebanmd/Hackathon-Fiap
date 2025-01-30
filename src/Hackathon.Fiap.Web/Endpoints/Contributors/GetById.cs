using Hackathon.Fiap.UseCases.Contributors.Get;

namespace Hackathon.Fiap.Web.Endpoints.Contributors;

/// <summary>
/// Get a Contributor by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Contributor record.
/// </remarks>
public class GetById(IMediator _mediator) : Endpoint<GetContributorByIdRequest, ContributorRecord>
{
    public override void Configure()
    {
        Get(GetContributorByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetContributorByIdRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var query = new GetContributorQuery(request.ContributorId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new ContributorRecord(result.Value.Id, result.Value.Name, result.Value.PhoneNumber);
        }
    }
}
