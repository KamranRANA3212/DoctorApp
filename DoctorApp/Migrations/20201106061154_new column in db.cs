using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class newcolumnindb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificates_Certificates_Certifiate_Id",
                table: "DoctorCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificates_Doctor_Doctor_Id",
                table: "DoctorCertificates");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "DoctorCertificates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Certifiate_Id",
                table: "DoctorCertificates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Doctor",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificates_Certificates_Certifiate_Id",
                table: "DoctorCertificates",
                column: "Certifiate_Id",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificates_Doctor_Doctor_Id",
                table: "DoctorCertificates",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificates_Certificates_Certifiate_Id",
                table: "DoctorCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificates_Doctor_Doctor_Id",
                table: "DoctorCertificates");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Doctor");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_Id",
                table: "DoctorCertificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Certifiate_Id",
                table: "DoctorCertificates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificates_Certificates_Certifiate_Id",
                table: "DoctorCertificates",
                column: "Certifiate_Id",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificates_Doctor_Doctor_Id",
                table: "DoctorCertificates",
                column: "Doctor_Id",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}