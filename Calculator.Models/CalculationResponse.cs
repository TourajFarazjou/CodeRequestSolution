using System;

namespace Calculator.Models
{
   public class CalculationResponse
   {
      public Guid Id { get; set; }

      public CalculationStatus Status { get; set; }

      public int Progress { get; set; }

      public int Outcome { get; set; }
   }
}
