using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stp.Data.Migrations
{
    public partial class Issue9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskAndSkill_TaskId",
                table: "TaskAndSkill");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestCategoryAndTest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestPosition = table.Column<int>(nullable: false),
                    TestCategoryId = table.Column<long>(nullable: false),
                    TestId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCategoryAndTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCategoryAndTest_TestCategory_TestCategoryId",
                        column: x => x.TestCategoryId,
                        principalTable: "TestCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCategoryAndTest_Test_TestId",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSection_TestId",
                table: "TestSection",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAndSkill_TaskId_SkillId",
                table: "TaskAndSkill",
                columns: new[] { "TaskId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                table: "Skill",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestCategoryAndTest_TestId",
                table: "TestCategoryAndTest",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCategoryAndTest_TestCategoryId_TestId",
                table: "TestCategoryAndTest",
                columns: new[] { "TestCategoryId", "TestId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSection_Test_TestId",
                table: "TestSection",
                column: "TestId",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSection_Test_TestId",
                table: "TestSection");

            migrationBuilder.DropTable(
                name: "TestCategoryAndTest");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropIndex(
                name: "IX_TestSection_TestId",
                table: "TestSection");

            migrationBuilder.DropIndex(
                name: "IX_TaskAndSkill_TaskId_SkillId",
                table: "TaskAndSkill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_Name",
                table: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAndSkill_TaskId",
                table: "TaskAndSkill",
                column: "TaskId");
        }
    }
}
