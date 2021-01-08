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

         Calculator.Tests

         _context.CalculatorJobs.Add(calculatorJob);
         await _context.SaveChangesAsync();

         BackgroundJob.Enqueue(() => RunInBackground(calculatorJob.Id));

         return calculatorJob.Id;
      }

      public async Task<CalculatorJob> GetCalculation(Guid jobId)
      {
         return await _context.CalculatorJobs.FirstOrDefaultAsync(e => e.Id == jobId);
      }


      /// <summary>
      /// https://www.youtube.com/watch?v=UAWDMYKy8PM
      /// </summary>
      public void RunInBackground(Guid jobId)
      {
         //Thread.Sleep(5000);
         
         //Console.WriteLine($"START Running {Thread.CurrentThread.Name}");

         // https://www.youtube.com/watch?v=DpLQYVErKm8

         var result = _context.CalculatorJobs
                         .FromSqlRaw<CalculatorJob>($"spStartCalculation '{jobId}'")
                         .ToList()
                         .FirstOrDefault();


         //Console.WriteLine($"{jobId} updated ******************* ");

         ////Thread.Sleep(5000);

         //Console.WriteLine($"END Running {Thread.CurrentThread.Name}");


      }
   }
}
