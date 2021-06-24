using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class io : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_Patient_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Patient_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Doctor_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Patient_Id",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Appointment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_User_Id",
                table: "Appointment",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_User_Id",
                table: "Appointment",
                column: "User_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_User_Id",
                table: "Appointment",
                column: "User_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_User_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_User_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_User_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Doctor_Id",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patient_Id",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Patient_Id",
                table: "Appointment",
                column: "Patient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_DoctorId",
                table: "Appointment",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_Doctor_Id",
                table: "Appointment",
                column: "Doctor_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_PatientId",
                table: "Appointment",
                column: "PatientId",
                principalTable: "Patient",
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
    }
}