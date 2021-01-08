using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Models;

namespace Calculator.Services
{
   public interface ICalculatorService
   {
      
      Task<IEnumerable<CalculationResponse>> GetCalculations();


      Task<Guid> StartCalculation(CalculationRequest request);

      Task<CalculationResponse> GetCalculationStatus(Guid jobId);
   }
}




