using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class irer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialities_Specialties_SpecialtiesId",
                table: "DoctorSpecialities");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "DoctorSpecialities");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialtiesId",
                table: "DoctorSpecialities",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialities_Specialties_SpecialtiesId",
                table: "DoctorSpecialities",
                column: "SpecialtiesId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialities_Specialties_SpecialtiesId",
                table: "DoctorSpecialities");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialtiesId",
                table: "DoctorSpecialities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "DoctorSpecialities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialities_Specialties_SpecialtiesId",
                table: "DoctorSpecialities",
                column: "SpecialtiesId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}