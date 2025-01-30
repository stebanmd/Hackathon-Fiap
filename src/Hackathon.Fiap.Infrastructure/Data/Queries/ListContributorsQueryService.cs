﻿using Hackathon.Fiap.UseCases.Contributors;
using Hackathon.Fiap.UseCases.Contributors.List;

namespace Hackathon.Fiap.Infrastructure.Data.Queries;

public class ListContributorsQueryService(AppDbContext _db) : IListContributorsQueryService
{
    // You can use EF, Dapper, SqlClient, etc. for queries -
    // this is just an example

    public async Task<IEnumerable<ContributorDto>> ListAsync()
    {
        // NOTE: This will fail if testing with EF InMemory provider!
        var result = await _db.Database.SqlQuery<ContributorDto>(
          $"SELECT Id, Name, PhoneNumber_Number AS PhoneNumber FROM Contributors") // don't fetch other big columns
          .ToListAsync();

        return result;
    }
}
