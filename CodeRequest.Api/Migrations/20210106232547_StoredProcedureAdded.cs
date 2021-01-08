using Microsoft.EntityFrameworkCore.Migrations;

namespace Calculator.Api.Migrations
{
    public partial class StoredProcedureAdded : Migration
    {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         var sql = @"
            IF OBJECT_ID('spStartCalculation', 'P') IS NOT NULL
            DROP PROC spStartCalculation
            GO
 
            CREATE PROCEDURE [dbo].[spStartCalculation]
                @JobId UNIQUEIDENTIFIER
            AS
            BEGIN
                
				UPDATE CalculatorJob
				SET Progress = 35
				WHERE id = @JobId;
              

            END";

         migrationBuilder.Sql(sql);

      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.Sql(@"DROP PROC spStartCalculation");
      }
   }
}
