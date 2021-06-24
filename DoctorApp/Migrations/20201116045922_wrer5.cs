using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class wrer5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_Patient_Id",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_Patient_Id",
                table: "Appointment",
                column: "Patient_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_Patient_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Doctor");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_Patient_Id",
                table: "Appointment",
                column: "Patient_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}