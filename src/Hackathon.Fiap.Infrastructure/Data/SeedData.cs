﻿using Hackathon.Fiap.Core.Aggregates.Doctors;
using Hackathon.Fiap.Core.Aggregates.Users;
using Hackathon.Fiap.UseCases.Doctors.Register;
using Hackathon.Fiap.UseCases.Patients.Register;

namespace Hackathon.Fiap.Infrastructure.Data;

public static class SeedData
{
    public static readonly Specialty Specialty1 = new() { Id = 1, Name = "Cardiologist" };
    public static readonly Specialty Specialty2 = new() { Id = 2, Name = "Geral" };
    public static readonly Specialty Specialty3 = new() { Id = 3, Name = "Pediatric" };


    public static readonly RegisterDoctorCommand RegisterMockedDoctorCommand = new(
        "John Doe",
        "12345678909",
        "123456-RS",
        "dr_johndoe@test.com",
        "str0ngP@assword",
        Specialty1.Id);

    public static readonly RegisterPatientCommand RegisterMockedPatientCommand = new(
        "Alice",
        "45678912364",
        "alice@test.com",
        "str0ngP@assword");


    public static async Task PopulateTestDataAsync(AppDbContext dbContext)
    {
        dbContext.Roles.AddRange
        (
            new ApplicationRoles()
            {
                Id = Guid.NewGuid().ToString(),
                Name = ApplicationRoles.Doctor,
                NormalizedName = ApplicationRoles.Doctor.ToUpper()
            },
            new ApplicationRoles()
            {
                Id = Guid.NewGuid().ToString(),
                Name = ApplicationRoles.Patient,
                NormalizedName = ApplicationRoles.Patient.ToUpper()
            }
        );

        dbContext.Specialties.AddRange(Specialty1, Specialty2, Specialty3);
        await dbContext.SaveChangesAsync();
    }
}
