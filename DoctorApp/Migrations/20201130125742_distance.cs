using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class distance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Appointment_Appointment_Id",
                table: "Notifications");

            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "Patient",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Patient",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Appointment_Id",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HospitalLong",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HospitalLat",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Appointment_Appointment_Id",
                table: "Notifications",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Appointment_Appointment_Id",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "Long",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Lat",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Appointment_Id",
                table: "Notifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "HospitalLong",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "HospitalLat",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Appointment_Appointment_Id",
                table: "Notifications",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}