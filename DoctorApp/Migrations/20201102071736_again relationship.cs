using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class againrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doctor_Id",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patient_Id",
                table: "Appointment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Patient_Id",
                table: "Appointment",
                column: "Patient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_Patient_Id",
                table: "Appointment",
                column: "Patient_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_Patient_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Patient_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Patient_Id",
                table: "Appointment");
        }
    }
}