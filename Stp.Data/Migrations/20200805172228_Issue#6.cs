using Microsoft.EntityFrameworkCore.Migrations;

namespace Stp.Data.Migrations
{
    public partial class Issue6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategory_TaskCategory_ParentId",
                table: "TaskCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCategory_TestCategory_ParentId",
                table: "TestCategory");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "TestCategory",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "TaskCategory",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategory_TaskCategory_ParentId",
                table: "TaskCategory",
                column: "ParentId",
                principalTable: "TaskCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestCategory_TestCategory_ParentId",
                table: "TestCategory",
                column: "ParentId",
                principalTable: "TestCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCategory_TaskCategory_ParentId",
                table: "TaskCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_TestCategory_TestCategory_ParentId",
                table: "TestCategory");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "TestCategory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "TaskCategory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCategory_TaskCategory_ParentId",
                table: "TaskCategory",
                column: "ParentId",
                principalTable: "TaskCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestCategory_TestCategory_ParentId",
                table: "TestCategory",
                column: "ParentId",
                principalTable: "TestCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
