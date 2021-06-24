using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class yui : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_User_Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_User_Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Transaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Transaction",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_User_Id",
                table: "Transaction",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_User_Id",
                table: "Transaction",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}