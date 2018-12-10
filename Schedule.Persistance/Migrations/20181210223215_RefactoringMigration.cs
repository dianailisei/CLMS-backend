using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Persistance.Migrations
{
    public partial class RefactoringMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Subjects",
                newName: "HeadOfDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                newName: "IX_Subjects_HeadOfDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_HeadOfDepartmentId",
                table: "Subjects",
                column: "HeadOfDepartmentId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_HeadOfDepartmentId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "HeadOfDepartmentId",
                table: "Subjects",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_HeadOfDepartmentId",
                table: "Subjects",
                newName: "IX_Subjects_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
