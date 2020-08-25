using Microsoft.EntityFrameworkCore.Migrations;

namespace Stp.Data.Migrations
{
    public partial class Issue24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskAndSkill_TaskId",
                table: "TaskAndSkill");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
