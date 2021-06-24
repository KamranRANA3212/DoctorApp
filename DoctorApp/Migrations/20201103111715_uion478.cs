using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class uion478 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Category_Category_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialties_Speciality_Id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Category_Id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Speciality_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Speciality_Id",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Category_Id",
                table: "Doctor",
                column: "Category_Id",
                unique: true,
                filter: "[Category_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                unique: true,
                filter: "[Speciality_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Category_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Speciality_Id",
                table: "Doctor");

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speciality_Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Category_Category_Id",
                table: "AspNetUsers",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialties_Speciality_Id",
                table: "AspNetUsers",
                column: "Speciality_Id",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}