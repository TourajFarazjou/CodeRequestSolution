using Calculator.Models;
using System;

namespace Calculator.Domain
{
   public class CalculatorJob
   {
      public Guid Id { get; set; }

      public int FirstValue { get; set; }

      public int SecondValue { get; set; }

      public CalculationStatus Status { get; set; }

      public int Progress { get; set; }

      public int Outcome { get; set; }

   }
}
