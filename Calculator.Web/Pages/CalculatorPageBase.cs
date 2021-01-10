using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Services;
using Calculator.Web.ViewModels;
using Calculator.Web.Components;

namespace Calculator.Web.Pages
{
	public class CalculatorPageBase : ComponentBase
   {
      [Inject]
      private ICalculatorService CalculatorService { get; set; }

      protected IEnumerable<CalculationResponse> CalculationJobs { get; set; }
      protected CalculationRequest CalculationRequest { get; set; }
      protected CalculationStatusViewModel CalculationStatus { get; set; }

      protected bool IsCalculating { get; set; }
      protected string IsCalculatingCssClass => IsCalculating ? null : "collapse";

      protected InputWatcher InputWatcher { get; set; }
      protected bool ContextValidated { get; set; }


		[Parameter]
		public EventCallback<CalculationRequest> CalculationRequestChanged { get; set; }

      public CalculatorPageBase()
      {
         CalculationRequest = new CalculationRequest();
         CalculationStatus = new CalculationStatusViewModel();
      }

      protected void FieldChanged(string fieldName)
      {
         ContextValidated = InputWatcher.Validate();
         CalculationRequestChanged.InvokeAsync(CalculationRequest);
      }

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
