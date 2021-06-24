using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class chnageinrelationshipp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount",
                column: "User_Id");
        }
    }
}