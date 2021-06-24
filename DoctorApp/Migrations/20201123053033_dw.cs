using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class dw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PMDCLicenceNumber",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "PMDCLicenceNumber",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}