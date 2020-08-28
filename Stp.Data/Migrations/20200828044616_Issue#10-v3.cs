using Microsoft.EntityFrameworkCore.Migrations;

namespace Stp.Data.Migrations
{
    public partial class Issue10v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSectionAndTasks_Task_TaskId",
                table: "TestSectionAndTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSectionAndTasks_TestSections_TestSectionId",
                table: "TestSectionAndTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSections",
                table: "TestSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSectionAndTasks",
                table: "TestSectionAndTasks");

            migrationBuilder.RenameTable(
                name: "TestSections",
                newName: "TestSection");

            migrationBuilder.RenameTable(
                name: "TestSectionAndTasks",
                newName: "TestSectionAndTask");

            migrationBuilder.RenameIndex(
                name: "IX_TestSectionAndTasks_TestSectionId",
                table: "TestSectionAndTask",
                newName: "IX_TestSectionAndTask_TestSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSectionAndTasks_TaskId",
                table: "TestSectionAndTask",
                newName: "IX_TestSectionAndTask_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSection",
                table: "TestSection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSectionAndTask",
                table: "TestSectionAndTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSectionAndTask_Task_TaskId",
                table: "TestSectionAndTask",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSectionAndTask_TestSection_TestSectionId",
                table: "TestSectionAndTask",
                column: "TestSectionId",
                principalTable: "TestSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSectionAndTask_Task_TaskId",
                table: "TestSectionAndTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSectionAndTask_TestSection_TestSectionId",
                table: "TestSectionAndTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSectionAndTask",
                table: "TestSectionAndTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSection",
                table: "TestSection");

            migrationBuilder.RenameTable(
                name: "TestSectionAndTask",
                newName: "TestSectionAndTasks");

            migrationBuilder.RenameTable(
                name: "TestSection",
                newName: "TestSections");

            migrationBuilder.RenameIndex(
                name: "IX_TestSectionAndTask_TestSectionId",
                table: "TestSectionAndTasks",
                newName: "IX_TestSectionAndTasks_TestSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSectionAndTask_TaskId",
                table: "TestSectionAndTasks",
                newName: "IX_TestSectionAndTasks_TaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSectionAndTasks",
                table: "TestSectionAndTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSections",
                table: "TestSections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSectionAndTasks_Task_TaskId",
                table: "TestSectionAndTasks",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSectionAndTasks_TestSections_TestSectionId",
                table: "TestSectionAndTasks",
                column: "TestSectionId",
                principalTable: "TestSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
