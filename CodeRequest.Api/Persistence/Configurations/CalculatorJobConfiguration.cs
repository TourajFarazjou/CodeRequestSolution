using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Calculator.Domain;

namespace Calculator.Persistence.Configurations
{
   public class CalculatorJobConfiguration : IEntityTypeConfiguration<CalculatorJob>
   {
      public void Configure(EntityTypeBuilder<CalculatorJob> builder)
      {

         builder.ToTable("CalculatorJob");

         builder.HasKey(t => t.Id);


         //builder.Sql(@"create trigger .....");
      }
   }
}
