using Calculator.Domain;
using Calculator.Models;
using Calculator.Persistence;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calculator.Services
{
   public class CalculatorService : ICalculatorService
   {
      private readonly CalculatorDBContext _context;

      public CalculatorService(CalculatorDBContext context)
      {
         _context = context;
      }


      public async Task<IEnumerable<CalculatorJob>> GetCalculations()
      {
         return await _context.CalculatorJobs.ToListAsync();
      }


      public async Task<Guid> StartCalculation(CalculatorJob calculatorJob)
      {
         _context.CalculatorJobs.Add(calculatorJob);
         await _context.SaveChangesAsync();

         BackgroundJob.Enqueue(() => RunInBackground(calculatorJob.Id));

         return calculatorJob.Id;
      }

      public async Task<CalculatorJob> GetCalculation(Guid jobId)
      {
         return await _context.CalculatorJobs.FirstOrDefaultAsync(e => e.Id == jobId);
      }

      public void RunInBackground(Guid jobId)
      {
         var result = _context.CalculatorJobs
                         .FromSqlRaw<CalculatorJob>($"spStartCalculation '{jobId}'")
                         .ToList()
                         .FirstOrDefault();
      }
   }
}
