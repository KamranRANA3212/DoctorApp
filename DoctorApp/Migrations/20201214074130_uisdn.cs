using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DoctorApp.Migrations
{
    public partial class uisdn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Category_Category_Id",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialties_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Category_Id",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Speciality_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Speciality_Id",
                table: "Doctor");

            migrationBuilder.CreateTable(
                name: "DoctorSpecialities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorId = table.Column<int>(nullable: false),
                    SpecialityId = table.Column<int>(nullable: false),
                    SpecialtiesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecialities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialities_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorSpecialities_Doctor_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialities_SpecialtiesId",
                table: "DoctorSpecialities",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialities_doctorId",
                table: "DoctorSpecialities",
                column: "doctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecialities");

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Speciality_Id",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

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