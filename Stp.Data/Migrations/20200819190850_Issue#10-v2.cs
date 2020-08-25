using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stp.Data.Migrations
{
    public partial class Issue10v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TestSections",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TestSectionAndTasks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskPosition = table.Column<int>(nullable: false),
                    TestSectionId = table.Column<long>(nullable: false),
                    TaskId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSectionAndTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSectionAndTasks_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSectionAndTasks_TestSections_TestSectionId",
                        column: x => x.TestSectionId,
                        principalTable: "TestSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSectionAndTasks_TaskId",
                table: "TestSectionAndTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSectionAndTasks_TestSectionId",
                table: "TestSectionAndTasks",
                column: "TestSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSectionAndTasks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TestSections");
        }
    }
}
