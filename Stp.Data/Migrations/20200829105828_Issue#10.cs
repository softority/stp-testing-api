using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stp.Data.Migrations
{
    public partial class Issue10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestSection",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    TestId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSectionAndTask",
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
                    table.PrimaryKey("PK_TestSectionAndTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSectionAndTask_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSectionAndTask_TestSection_TestSectionId",
                        column: x => x.TestSectionId,
                        principalTable: "TestSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSectionAndTask_TaskId",
                table: "TestSectionAndTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSectionAndTask_TestSectionId",
                table: "TestSectionAndTask",
                column: "TestSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSectionAndTask");

            migrationBuilder.DropTable(
                name: "TestSection");
        }
    }
}
