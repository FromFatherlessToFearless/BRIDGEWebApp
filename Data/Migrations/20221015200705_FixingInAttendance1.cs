using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRIDGEWebApp.Data.Migrations
{
    public partial class FixingInAttendance1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InAttendance",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "InAttendance",
                table: "ParticipantAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InAttendance",
                table: "ParticipantAttendances");

            migrationBuilder.AddColumn<bool>(
                name: "InAttendance",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
