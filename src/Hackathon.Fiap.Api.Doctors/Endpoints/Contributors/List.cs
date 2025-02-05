﻿using Hackathon.Fiap.UseCases.Contributors.List;

namespace Hackathon.Fiap.Api.Doctors.Endpoints.Contributors;

/// <summary>
/// List all Contributors
/// </summary>
/// <remarks>
/// List all contributors - returns a ContributorListResponse containing the Contributors.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<ContributorListResponse>
{
    public override void Configure()
    {
        Get("/Contributors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var result = await _mediator.Send(new ListContributorsQuery(null, null), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new ContributorListResponse
            {
                Contributors = result.Value.Select(c => new ContributorRecord(c.Id, c.Name, c.PhoneNumber)).ToList()
            };
        }
    }
}
