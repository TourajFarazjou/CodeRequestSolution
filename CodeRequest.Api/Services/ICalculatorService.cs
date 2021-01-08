using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculator.Domain;
using Calculator.Models;

namespace Calculator.Services
{
   public interface ICalculatorService
   {

      Task<IEnumerable<CalculatorJob>> GetCalculations();





      Task<Guid> StartCalculation(CalculatorJob calculatorJob);

      Task<CalculatorJob> GetCalculation(Guid jobId);


   }
}




