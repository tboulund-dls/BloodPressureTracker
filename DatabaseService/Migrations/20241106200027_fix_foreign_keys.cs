using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseService.Migrations
{
    /// <inheritdoc />
    public partial class fix_foreign_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Doctors_SeenDoctorId",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "SeenDoctorId",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Doctors_SeenDoctorId",
                table: "Measurements",
                column: "SeenDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Doctors_SeenDoctorId",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "SeenDoctorId",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Doctors_SeenDoctorId",
                table: "Measurements",
                column: "SeenDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
