using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class dwijd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "DoctorAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "DoctorAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "DoctorAddress");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "DoctorAddress");
        }
    }
}