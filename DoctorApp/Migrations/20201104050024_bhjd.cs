using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DoctorApp.Migrations
{
    public partial class bhjd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CheckUpFee",
                table: "Doctor",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Examine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    NextVisitDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Appointment_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examine_Appointment_Appointment_Id",
                        column: x => x.Appointment_Id,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examine_Appointment_Id",
                table: "Examine",
                column: "Appointment_Id",
                unique: true,
                filter: "[Appointment_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examine");

            migrationBuilder.DropColumn(
                name: "CheckUpFee",
                table: "Doctor");
        }
    }
}