using Hackathon.Fiap.UseCases.Contributors.Create;

namespace Hackathon.Fiap.Web.Endpoints.Contributors;

/// <summary>
/// Create a new Contributor
/// </summary>
/// <remarks>
/// Creates a new Contributor given a name.
/// </remarks>
public class Create(IMediator _mediator) : Endpoint<CreateContributorRequest, CreateContributorResponse>
{
    public override void Configure()
    {
        Post(CreateContributorRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CreateContributorRequest { Name = "Contributor Name" };
        });
    }

    public override async Task HandleAsync(CreateContributorRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await _mediator.Send(new CreateContributorCommand(request.Name!,
          request.PhoneNumber), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new CreateContributorResponse(result.Value, request.Name!);
            return;
        }
        // TODO: Handle other cases as necessary
    }
}
