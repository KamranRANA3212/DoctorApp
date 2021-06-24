using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class iue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalLocation",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "IsLicenceVerified",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "LicenceNumber",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Patient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "HospitalLocation",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLicenceVerified",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LicenceNumber",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}