using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Fiap.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddDefaultRoles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: ["Id", "Name", "NormalizedName"],
            values: new object[,]
                {
                    { Guid.NewGuid().ToString(), ApplicationRoles.Doctor, ApplicationRoles.Doctor.ToUpper() },
                    { Guid.NewGuid().ToString(), ApplicationRoles.Patient, ApplicationRoles.Patient.ToUpper() },
                }
            );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Name",
            keyValues: [ApplicationRoles.Doctor, ApplicationRoles.Patient]
        );
    }
}
