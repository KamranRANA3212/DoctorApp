using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class tyu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_AspNetUsers_ApplicationUserId",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_ApplicationUserId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BankAccount");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_AspNetUsers_User_Id",
                table: "BankAccount",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_AspNetUsers_User_Id",
                table: "BankAccount");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_User_Id",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "BankAccount");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "BankAccount",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_ApplicationUserId",
                table: "BankAccount",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_AspNetUsers_ApplicationUserId",
                table: "BankAccount",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}