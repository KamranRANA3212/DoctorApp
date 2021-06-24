using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class chbage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "TransactionStatus",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Transaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceCharges",
                table: "Appointment",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Appointment",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Category_Id",
                table: "Doctor",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor",
                column: "Speciality_Id",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "Status",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ServiceCharges",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Appointment");

            migrationBuilder.AddColumn<bool>(
                name: "TransactionStatus",
                table: "Transaction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}