using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Fiap.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class CreateSpecialtiesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "SpecialtyId",
            table: "Doctors",
            type: "int",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Specialties",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specialties", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Doctors_SpecialtyId",
            table: "Doctors",
            column: "SpecialtyId");

        migrationBuilder.AddForeignKey(
            name: "FK_Doctors_Specialties_SpecialtyId",
            table: "Doctors",
            column: "SpecialtyId",
            principalTable: "Specialties",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Doctors_Specialties_SpecialtyId",
            table: "Doctors");

        migrationBuilder.DropTable(
            name: "Specialties");

        migrationBuilder.DropIndex(
            name: "IX_Doctors_SpecialtyId",
            table: "Doctors");

        migrationBuilder.DropColumn(
            name: "SpecialtyId",
            table: "Doctors");
    }
}
