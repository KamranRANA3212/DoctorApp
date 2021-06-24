using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorApp.Migrations
{
    public partial class newtabletimeschedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorTimeSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Id = table.Column<string>(nullable: true),
                    Day_Id = table.Column<int>(nullable: true),
                    ShiftType = table.Column<int>(nullable: false),
                    TimeIn = table.Column<string>(nullable: true),
                    TimeOut = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTimeSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorTimeSchedule_Days_Day_Id",
                        column: x => x.Day_Id,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorTimeSchedule_AspNetUsers_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTimeSchedule_Day_Id",
                table: "DoctorTimeSchedule",
                column: "Day_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTimeSchedule_Doctor_Id",
                table: "DoctorTimeSchedule",
                column: "Doctor_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorTimeSchedule");
        }
    }
}