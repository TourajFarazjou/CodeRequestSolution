using AutoMapper;
using Calculator.Domain;
using Calculator.Models;
using Calculator.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CalculatorController : ControllerBase
   {
      private readonly ICalculatorService _calculatorService;
      private readonly IMapper _mapper;

      public CalculatorController(ICalculatorService calculatorService, IMapper mapper)
      {
         _calculatorService = calculatorService;
         _mapper = mapper;
      }

      [HttpGet("")]
      public async Task<IActionResult> GetCalculations()
      {
         var calculations = await _calculatorService.GetCalculations();
         var response = _mapper.Map<IEnumerable<CalculationResponse>>(calculations);
         return Ok(response);
      }

      [HttpPost]
      public async Task<IActionResult> StartCalculation(CalculationRequest request)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(request);
         }
         var newCalculatorJob = _mapper.Map<CalculatorJob>(request);
         var guid = await _calculatorService.StartCalculation(newCalculatorJob);
         return Ok(guid);
      }

      [HttpGet("{id:guid}")]
      public async Task<IActionResult> GetStatus(Guid id)
      {
         var calculatorJob = await _calculatorService.GetCalculation(id);
         if (calculatorJob == null)
         {
            return NotFound();
         }
         var statusObject = _mapper.Map<CalculationResponse>(calculatorJob);
         return Ok(statusObject);
      }
   }
}
