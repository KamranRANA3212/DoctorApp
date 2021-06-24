using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DoctorApp.Migrations
{
    public partial class wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Appointment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    ServiceCharges = table.Column<decimal>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    User_Id = table.Column<string>(nullable: true),
                    Appointment_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_Appointment_Appointment_Id",
                        column: x => x.Appointment_Id,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wallet_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    TransactionStatus = table.Column<bool>(nullable: false),
                    User_Id = table.Column<string>(nullable: true),
                    Wallet_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Wallet_Wallet_Id",
                        column: x => x.Wallet_Id,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_TransactionId",
                table: "Appointment",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_User_Id",
                table: "Transaction",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Wallet_Id",
                table: "Transaction",
                column: "Wallet_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Appointment_Id",
                table: "Wallet",
                column: "Appointment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_User_Id",
                table: "Wallet",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Transaction_TransactionId",
                table: "Appointment",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Transaction_TransactionId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_TransactionId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Appointment");
        }
    }
}