using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class changeindoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Appointment_Appointment_Id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctor_Doctor_Id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Patient_Patient_Id",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Appointment_Id",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistantName",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssistantNumber",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Appointment_Appointment_Id",
                table: "Reviews",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctor_Doctor_Id",
                table: "Reviews",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Patient_Patient_Id",
                table: "Reviews",
                column: "Patient_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Appointment_Appointment_Id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctor_Doctor_Id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Patient_Patient_Id",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AssistantName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "AssistantNumber",
                table: "Doctor");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Appointment_Id",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Appointment_Appointment_Id",
                table: "Reviews",
                column: "Appointment_Id",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctor_Doctor_Id",
                table: "Reviews",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Patient_Patient_Id",
                table: "Reviews",
                column: "Patient_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}