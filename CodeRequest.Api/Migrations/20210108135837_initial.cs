using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatorJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstValue = table.Column<int>(nullable: false),
                    SecondValue = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Progress = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorJob", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatorJob");
        }
    }
}
