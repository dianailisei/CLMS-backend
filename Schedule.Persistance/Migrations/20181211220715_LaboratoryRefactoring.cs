using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Persistance.Migrations
{
    public partial class LaboratoryRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laboratories_Subjects_SubjectId",
                table: "Laboratories");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Laboratories",
                newName: "ParentSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Laboratories_SubjectId",
                table: "Laboratories",
                newName: "IX_Laboratories_ParentSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratories_Subjects_ParentSubjectId",
                table: "Laboratories",
                column: "ParentSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laboratories_Subjects_ParentSubjectId",
                table: "Laboratories");

            migrationBuilder.RenameColumn(
                name: "ParentSubjectId",
                table: "Laboratories",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Laboratories_ParentSubjectId",
                table: "Laboratories",
                newName: "IX_Laboratories_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratories_Subjects_SubjectId",
                table: "Laboratories",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
