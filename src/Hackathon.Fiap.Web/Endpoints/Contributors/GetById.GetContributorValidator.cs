﻿using FluentValidation;

namespace Hackathon.Fiap.Web.Endpoints.Contributors;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class GetContributorValidator : Validator<GetContributorByIdRequest>
{
    public GetContributorValidator()
    {
        RuleFor(x => x.ContributorId)
          .GreaterThan(0);
    }
}
