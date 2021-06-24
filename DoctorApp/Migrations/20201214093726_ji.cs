using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class ji : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorExperience",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    Designation = table.Column<string>(nullable: true),
                    Hospital = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorExperience_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorQualification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(nullable: false),
                    Degree = table.Column<string>(nullable: true),
                    Institute = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorQualification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorQualification_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExperience_DoctorId",
                table: "DoctorExperience",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorQualification_DoctorId",
                table: "DoctorQualification",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorExperience");

            migrationBuilder.DropTable(
                name: "DoctorQualification");
        }
    }
}
