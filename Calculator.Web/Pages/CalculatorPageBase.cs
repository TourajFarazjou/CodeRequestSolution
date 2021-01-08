using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Services;
using Calculator.Web.ViewModels;

namespace Calculator.Web.Pages
{
   public class CalculatorPageBase : ComponentBase
   {
      [Inject]
      public ICalculatorService CalculatorService { get; set; }

      public IEnumerable<CalculationResponse> CalculationJobs { get; set; }
      public CalculationRequest CalculationRequest { get; set; }
      public CalculationStatusViewModel CalculationStatus { get; set; }

      public bool IsCalculating { get; set; }
      public string IsCalculatingCssClass => IsCalculating ? null : "collapse";

      public CalculatorPageBase()
      {
         CalculationRequest = new CalculationRequest();
         CalculationStatus = new CalculationStatusViewModel();
      }

      //protected override Task OnInitializedAsync()
      //{
      //}

      protected async Task HandleValidSubmit()
      {
         try
         {
            CalculationStatus = new CalculationStatusViewModel();
            IsCalculating = true;

            var jobId = await CalculatorService.StartCalculation(CalculationRequest);
            await Task.Run(() => // TODO: make it cancelable !
            {
               CalculationResponse statusObject;
               do
               {
                  statusObject = CalculatorService.GetCalculationStatus(jobId).Result;

                  CalculationStatus.Status = statusObject.Status.ToString();
                  CalculationStatus.Progress = $"{ statusObject.Progress } %";
                  
                  InvokeAsync(() => StateHasChanged());

                  // sleep (1000); ?

                  // OR use timer !!?

               } while (statusObject.Status == Models.CalculationStatus.Running);

               if (statusObject.Status == Models.CalculationStatus.Completed)
               {
                  CalculationStatus.Outcome = statusObject.Outcome;
               }
            });
         }
         finally
         {
            IsCalculating = false;

            StateHasChanged();
         }
      }
   }
}
