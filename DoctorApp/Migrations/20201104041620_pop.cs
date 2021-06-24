using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class pop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Transaction_TransactionId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_TransactionId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Appointment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_TransactionId",
                table: "Appointment",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Transaction_TransactionId",
                table: "Appointment",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}