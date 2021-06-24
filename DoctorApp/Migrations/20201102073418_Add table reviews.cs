using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DoctorApp.Migrations
{
    public partial class Addtablereviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EntryDateTime = table.Column<DateTime>(nullable: false),
                    Patient_Id = table.Column<string>(nullable: true),
                    Doctor_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Doctor_Id",
                table: "Reviews",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Patient_Id",
                table: "Reviews",
                column: "Patient_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}