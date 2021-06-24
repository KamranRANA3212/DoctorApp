using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class newcolumnsinbankavvount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "BankAccount");

            migrationBuilder.AddColumn<long>(
                name: "ExpiryMonth",
                table: "BankAccount",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExpiryYear",
                table: "BankAccount",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Stripe_Customer_Token",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stripe_Token",
                table: "BankAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryMonth",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "ExpiryYear",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Stripe_Customer_Token",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Stripe_Token",
                table: "BankAccount");

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "BankAccount",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}