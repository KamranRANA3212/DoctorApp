using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class rr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_ApplicationUserId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_ApplicationUserId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Appointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ApplicationUserId",
                table: "Appointment",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_ApplicationUserId",
                table: "Appointment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}