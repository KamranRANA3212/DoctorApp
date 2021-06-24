using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class opid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "HospitalLat",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalLong",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalLat",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HospitalLong",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}