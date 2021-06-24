using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class newtablecertififcation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Id = table.Column<int>(nullable: true),
                    Certifiate_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorCertificates_Certificates_Certifiate_Id",
                        column: x => x.Certifiate_Id,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorCertificates_Doctor_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCertificates_Certifiate_Id",
                table: "DoctorCertificates",
                column: "Certifiate_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCertificates_Doctor_Id",
                table: "DoctorCertificates",
                column: "Doctor_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorCertificates");

            migrationBuilder.DropTable(
                name: "Certificates");
        }
    }
}