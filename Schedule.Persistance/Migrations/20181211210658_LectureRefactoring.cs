using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Persistance.Migrations
{
    public partial class LectureRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Subjects_SubjectId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Lectures_LectureId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_LectureId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Lectures",
                newName: "ParentSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Lectures_SubjectId",
                table: "Lectures",
                newName: "IX_Lectures_ParentSubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "Lectures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Teachers_LecturerId",
                table: "Lectures",
                column: "LecturerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Subjects_ParentSubjectId",
                table: "Lectures",
                column: "ParentSubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Teachers_LecturerId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Subjects_ParentSubjectId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "ParentSubjectId",
                table: "Lectures",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Lectures_ParentSubjectId",
                table: "Lectures",
                newName: "IX_Lectures_SubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "LectureId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_LectureId",
                table: "Teachers",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Subjects_SubjectId",
                table: "Lectures",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Lectures_LectureId",
                table: "Teachers",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
