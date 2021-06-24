using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class ftsfsf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Wallet_Wallet_Id",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Appointment_Appointment_Id",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Appointment_Id",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ServiceCharges",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Wallet_Id",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Appointment_Id",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StripeTransactionId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "Appointment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Appointment_Id",
                table: "Transaction",
                column: "Appointment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_WalletId",
                table: "Appointment",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Wallet_WalletId",
                table: "Appointment",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Appointment_Appointment_Id",
                table: "Transaction",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Wallet_WalletId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Appointment_Appointment_Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Appointment_Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_WalletId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Appointment_Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "StripeTransactionId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "Appointment_Id",
                table: "Wallet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceCharges",
                table: "Wallet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Wallet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Wallet_Id",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet",
                column: "Appointment_Id",
                unique: true,
                filter: "[Appointment_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction",
                column: "Wallet_Id",
                unique: true,
                filter: "[Wallet_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Wallet_Wallet_Id",
                table: "Transaction",
                column: "Wallet_Id",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Appointment_Appointment_Id",
                table: "Wallet",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}