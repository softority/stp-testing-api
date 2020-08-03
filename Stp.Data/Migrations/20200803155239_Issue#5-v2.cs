using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stp.Data.Migrations
{
    public partial class Issue5v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultichoiceTaskAnswer_StpTask_TaskId",
                table: "MultichoiceTaskAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StpTask",
                table: "StpTask");

            migrationBuilder.DropColumn(
                name: "ExerciseCategoryId",
                table: "StpTask");

            migrationBuilder.DropColumn(
                name: "TestSectionId",
                table: "StpTask");

            migrationBuilder.RenameTable(
                name: "StpTask",
                newName: "Task");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Task",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Task",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TaskCategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCategory_TaskCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TaskCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_CategoryId",
                table: "Task",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCategory_ParentId",
                table: "TaskCategory",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultichoiceTaskAnswer_Task_TaskId",
                table: "MultichoiceTaskAnswer",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_CategoryId",
                table: "Task",
                column: "CategoryId",
                principalTable: "TaskCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultichoiceTaskAnswer_Task_TaskId",
                table: "MultichoiceTaskAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_CategoryId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "TaskCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_CategoryId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "StpTask");

            migrationBuilder.AddColumn<long>(
                name: "ExerciseCategoryId",
                table: "StpTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TestSectionId",
                table: "StpTask",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StpTask",
                table: "StpTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MultichoiceTaskAnswer_StpTask_TaskId",
                table: "MultichoiceTaskAnswer",
                column: "TaskId",
                principalTable: "StpTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
