using Microsoft.EntityFrameworkCore.Migrations;

namespace Stp.Data.Migrations
{
    public partial class Issue18v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Task",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
