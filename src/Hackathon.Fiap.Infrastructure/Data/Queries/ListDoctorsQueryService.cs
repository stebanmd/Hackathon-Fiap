﻿using Hackathon.Fiap.UseCases.Doctors;
using Hackathon.Fiap.UseCases.Doctors.List;

namespace Hackathon.Fiap.Infrastructure.Data.Queries;

public class ListDoctorsQueryService(AppDbContext _db) : IListDoctorsQueryService
{
    // You can use EF, Dapper, SqlClient, etc. for queries -
    // this is just an example

    public async Task<IEnumerable<DoctorDto>> ListAsync(int? specialtyId)
    {
        // NOTE: This will fail if testing with EF InMemory provider!
        var result = await _db.Database
            .SqlQuery<DoctorDto>($"SELECT Id, Name, Cpf, Crm, SpecialtyId FROM Doctors")
            .Where(x => specialtyId == null || x.SpecialtyId == specialtyId)
            .ToListAsync();

        return result;
    }
}
