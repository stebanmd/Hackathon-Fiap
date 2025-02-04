using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Fiap.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InsertDefaultSpecialties : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Specialties",
            columns: ["Id", "Name"],
            values: [1, "General"]);

        migrationBuilder.InsertData(
            table: "Specialties",
            columns: ["Id", "Name"],
            values: [2, "Cardiology"]);

        migrationBuilder.InsertData(
            table: "Specialties",
            columns: ["Id", "Name"],
            values: [3, "Ophthalmology"]);

        migrationBuilder.InsertData(
            table: "Specialties",
            columns: ["Id", "Name"],
            values: [4, "Psychiatry"]);

        migrationBuilder.InsertData(
            table: "Specialties",
            columns: ["Id", "Name"],
            values: [5, "Dermatology"]);
    }
}
