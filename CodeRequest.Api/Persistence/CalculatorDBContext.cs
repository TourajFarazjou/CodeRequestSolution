using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Calculator.Domain;

namespace Calculator.Persistence
{
   // https://www.oracletutorial.com/getting-started/oracle-sample-database/
   // https://github.com/jasontaylordev/CleanArchitecture/blob/198dd46728e6acd5fee86f6286a0deb9c45b4003/src/Infrastructure/Persistence/ApplicationDbContext.cs

   public partial class CalculatorDBContext : DbContext
   {
      public CalculatorDBContext(DbContextOptions<CalculatorDBContext> options)
         : base(options)
      {
      }

      

      public virtual DbSet<CalculatorJob> CalculatorJobs { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {

      }


      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

         //modelBuilder.SeedSampleData();
      }
   }
}
