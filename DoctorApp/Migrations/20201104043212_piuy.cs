using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class piuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_User_Id",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_User_Id",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet",
                column: "Appointment_Id",
                unique: true,
                filter: "[Appointment_Id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Wallet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet",
                column: "Appointment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_User_Id",
                table: "Wallet",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_User_Id",
                table: "Wallet",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}