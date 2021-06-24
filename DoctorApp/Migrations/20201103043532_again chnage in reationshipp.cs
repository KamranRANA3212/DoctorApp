using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class againchnageinreationshipp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Category_Id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Speciality_Id",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Category_Id",
                table: "AspNetUsers",
                column: "Category_Id",
                unique: true,
                filter: "[Category_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Speciality_Id",
                table: "AspNetUsers",
                column: "Speciality_Id",
                unique: true,
                filter: "[Speciality_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Category_Id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Speciality_Id",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Category_Id",
                table: "AspNetUsers",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Speciality_Id",
                table: "AspNetUsers",
                column: "Speciality_Id");
        }
    }
}