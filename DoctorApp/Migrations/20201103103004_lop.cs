using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class lop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_User_Id",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_User_Id",
                table: "Patient",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_User_Id",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_User_Id",
                table: "Patient",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_User_Id",
                table: "Doctor",
                column: "User_Id");
        }
    }
}