using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackathon.Fiap.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class CreateDoctorAppointmentConfigurationTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DoctorAppointmentConfiguration",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                DoctorId = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<double>(type: "float", nullable: false),
                Duration = table.Column<TimeOnly>(type: "time", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DoctorAppointmentConfiguration", x => x.Id);
                table.ForeignKey(
                    name: "FK_DoctorAppointmentConfiguration_Doctors_DoctorId",
                    column: x => x.DoctorId,
                    principalTable: "Doctors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DoctorAppointmentConfiguration_DoctorId",
            table: "DoctorAppointmentConfiguration",
            column: "DoctorId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DoctorAppointmentConfiguration");
    }
}
