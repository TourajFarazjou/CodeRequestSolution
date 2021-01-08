using Calculator.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculator.Services
{
   public class CalculatorService : ICalculatorService
   {
      private readonly HttpClient _httpClient;

      public CalculatorService(HttpClient httpClient)
      {
         _httpClient = httpClient;
      }

      public async Task<IEnumerable<CalculationResponse>> GetCalculations()
      {
         return await _httpClient.GetJsonAsync<IEnumerable<CalculationResponse>>("api/calculator");
      }



      public async Task<Guid> StartCalculation(CalculationRequest request)
      {

         return await _httpClient.PostJsonAsync<Guid>("api/calculator", request);

      }

      public async Task<CalculationResponse> GetCalculationStatus(Guid jobId)
      {
         return await _httpClient.GetJsonAsync<CalculationResponse>($"api/calculator/{jobId}");
      }
   }
}
