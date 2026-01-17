using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patientsTables",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientsTables", x => x.SSN);
                });

            migrationBuilder.CreateTable(
                name: "measurementsTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    PatientSSN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measurementsTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_measurementsTables_patientsTables_PatientSSN",
                        column: x => x.PatientSSN,
                        principalTable: "patientsTables",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_measurementsTables_PatientSSN",
                table: "measurementsTables",
                column: "PatientSSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "measurementsTables");

            migrationBuilder.DropTable(
                name: "patientsTables");
        }
    }
}
