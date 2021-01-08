using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Api.Migrations
{
    public partial class modelchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstValue",
                table: "CalculatorJob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondValue",
                table: "CalculatorJob",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstValue",
                table: "CalculatorJob");

            migrationBuilder.DropColumn(
                name: "SecondValue",
                table: "CalculatorJob");
        }
    }
}
