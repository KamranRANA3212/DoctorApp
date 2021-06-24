using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class iooi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor");

            migrationBuilder.AlterColumn<int>(
                name: "Message",
                table: "Notifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}