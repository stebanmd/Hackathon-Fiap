using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Fiap.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddReasonColumnInAppointments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Reason",
            table: "Appointments",
            type: "nvarchar(500)",
            maxLength: 500,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Reason",
            table: "Appointments");
    }
}
