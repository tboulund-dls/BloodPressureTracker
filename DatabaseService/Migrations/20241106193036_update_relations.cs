using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseService.Migrations
{
    /// <inheritdoc />
    public partial class update_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Measurements_MeasurementsMeasurementId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MeasurementsMeasurementId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MeasurementsMeasurementId",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "PatientSsn",
                table: "Measurements",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PatientSsn",
                table: "Measurements",
                column: "PatientSsn");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Patients_PatientSsn",
                table: "Measurements",
                column: "PatientSsn",
                principalTable: "Patients",
                principalColumn: "Ssn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Patients_PatientSsn",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PatientSsn",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PatientSsn",
                table: "Measurements");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementsMeasurementId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MeasurementsMeasurementId",
                table: "Patients",
                column: "MeasurementsMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Measurements_MeasurementsMeasurementId",
                table: "Patients",
                column: "MeasurementsMeasurementId",
                principalTable: "Measurements",
                principalColumn: "MeasurementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
