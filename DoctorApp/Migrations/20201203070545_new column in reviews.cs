using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class newcolumninreviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelBy",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Appointment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelBy",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Appointment");
        }
    }
}