using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class bb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction",
                column: "Wallet_Id",
                unique: true,
                filter: "[Wallet_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction",
                column: "Wallet_Id");
        }
    }
}