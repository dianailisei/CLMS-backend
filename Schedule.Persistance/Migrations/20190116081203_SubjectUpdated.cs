using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Persistance.Migrations
{
    public partial class SubjectUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Year",
                table: "Subjects",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Subjects");
        }
    }
}
