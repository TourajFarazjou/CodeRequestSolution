using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Models
{
   public class CalculationRequest
   {
      [Required]
      [Range(20, 500, ErrorMessage = "The value must be between 20 to 500")]
      public int FirstValue { get; set; }

      [Required]
      [Range(800, 1400, ErrorMessage = "The value must be between 800 to 1400")]
      public int SecondValue { get; set; }
   }
}
