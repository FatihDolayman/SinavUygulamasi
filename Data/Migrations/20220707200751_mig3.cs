using Microsoft.EntityFrameworkCore.Migrations;

namespace SinavUygulamasi.Data.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Question_QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResult_Exam_ExamId",
                table: "UserExamResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExamResult",
                table: "UserExamResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer");

            migrationBuilder.RenameTable(
                name: "UserExamResult",
                newName: "UserExamResults");

            migrationBuilder.RenameTable(
                name: "UserAnswer",
                newName: "UserAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserExamResult_ExamId",
                table: "UserExamResults",
                newName: "IX_UserExamResults_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_QuestionId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExamResults",
                table: "UserExamResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Question_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResults_Exam_ExamId",
                table: "UserExamResults",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Question_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExamResults_Exam_ExamId",
                table: "UserExamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExamResults",
                table: "UserExamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.RenameTable(
                name: "UserExamResults",
                newName: "UserExamResult");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "UserAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_UserExamResults_ExamId",
                table: "UserExamResult",
                newName: "IX_UserExamResult_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExamResult",
                table: "UserExamResult",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Question_QuestionId",
                table: "UserAnswer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExamResult_Exam_ExamId",
                table: "UserExamResult",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
