using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class chnagess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTimeSchedule_Days_Day_Id",
                table: "DoctorTimeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTimeSchedule_AspNetUsers_Doctor_Id",
                table: "DoctorTimeSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "DoctorTimeSchedule",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Day_Id",
                table: "DoctorTimeSchedule",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTimeSchedule_Days_Day_Id",
                table: "DoctorTimeSchedule",
                column: "Day_Id",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTimeSchedule_Doctor_Doctor_Id",
                table: "DoctorTimeSchedule",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTimeSchedule_Days_Day_Id",
                table: "DoctorTimeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorTimeSchedule_Doctor_Doctor_Id",
                table: "DoctorTimeSchedule");

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Id",
                table: "DoctorTimeSchedule",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Day_Id",
                table: "DoctorTimeSchedule",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTimeSchedule_Days_Day_Id",
                table: "DoctorTimeSchedule",
                column: "Day_Id",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorTimeSchedule_AspNetUsers_Doctor_Id",
                table: "DoctorTimeSchedule",
                column: "Doctor_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}