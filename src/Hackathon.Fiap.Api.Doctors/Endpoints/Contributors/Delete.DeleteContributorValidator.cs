﻿namespace Hackathon.Fiap.Api.Doctors.Endpoints.Contributors;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class DeleteContributorValidator : Validator<DeleteContributorRequest>
{
    public DeleteContributorValidator()
    {
        RuleFor(x => x.ContributorId)
          .GreaterThan(0);
    }
}
