﻿namespace Hackathon.Fiap.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDto>>;
