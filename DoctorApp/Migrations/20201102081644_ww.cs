using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class ww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeIn",
                table: "DoctorTimeSchedule");

            migrationBuilder.DropColumn(
                name: "TimeOut",
                table: "DoctorTimeSchedule");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "DoctorTimeSchedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "DoctorTimeSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "DoctorTimeSchedule");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "DoctorTimeSchedule");

            migrationBuilder.AddColumn<string>(
                name: "TimeIn",
                table: "DoctorTimeSchedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeOut",
                table: "DoctorTimeSchedule",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}