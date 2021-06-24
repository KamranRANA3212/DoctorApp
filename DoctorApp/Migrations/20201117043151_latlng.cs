using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class latlng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Doctor");
        }
    }
}