using Calculator.Domain;
using Calculator.Models;
using Calculator.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CalculatorController : ControllerBase
   {
      private readonly ICalculatorService _calculatorService;

      public CalculatorController(ICalculatorService calculatorService)
      {
         _calculatorService = calculatorService;
      }


      [HttpGet("")]
      public async Task<IActionResult> GetCalculations()
      {

         var calculations = await _calculatorService.GetCalculations();
         List<CalculationResponse> response = new List<CalculationResponse>();
         foreach (var item in calculations)
         {
            response.Add(new CalculationResponse { Id = item.Id });
         }
         return Ok(response);

      }


      [HttpPost]
      public async Task<IActionResult> StartCalculation(CalculationRequest request)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(request);
         }

         // map to domain object
         var newJob = new CalculatorJob
         {
            FirstValue = request.FirstValue,
            SecondValue = request.SecondValue,
         };

         var guid = await _calculatorService.StartCalculation(newJob);

         return Ok(guid);
      }

      [HttpGet("{guid}")]
      public async Task<IActionResult> GetStatus(Guid guid)
      {
         var calculatorJob = await _calculatorService.GetCalculation(guid);
         if (calculatorJob == null)
         {
            return NotFound();
         }

         // map to status ...
         var statusObject = new CalculationResponse
         {
            Id = calculatorJob.Id,
            Status = calculatorJob.Status, //.ToString(),
            Progress = calculatorJob.Progress,
            Outcome = calculatorJob.Outcome
         };

         return Ok(statusObject);
      }
   }
}
