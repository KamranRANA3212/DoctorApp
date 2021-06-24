using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DoctorApp.Migrations
{
    public partial class manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "Appointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Specialties",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Patient",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Doctor",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Category",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Appointment_Id",
                table: "Appointment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Appointment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Appointment_Id",
                table: "Appointment",
                column: "Appointment_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_Appointment_Id",
                table: "Appointment",
                column: "Appointment_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_Appointment_Id",
                table: "Appointment",
                column: "Appointment_Id",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_Appointment_Id",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_Appointment_Id",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_Appointment_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Appointment_Id",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Appointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Specialties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}