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


      protected bool IsCalculating { get; set; }
      public string IsCalculatingCssClass => IsCalculating ? null : "collapse";


      //protected bool DoneCalculating { get; set; }
      //public string DoneCalculatingCssClass => IsCalculating ? null : "collapse";


      //private bool isCalculating;
      //public bool IsCalculating 
      //{
      //   get { return isCalculating; }
      //   set 
      //   {
      //      IsCalculatingCssClass
      //   } 
      //}

      //protected string Hidden { get; set; } = "visibility: hidden";

      //protected string  ProgressIndicator CssClass => collapseProgressIndicator ? "collapse" : null;

      //private void ToggleProgressIndicator(bool hide)
      //{
      //   collapseProgressIndicator = !collapseProgressIndicator;
      //}


      public CalculatorPageBase()
      {
         CalculationRequest = new CalculationRequest();
         CalculationStatus = new CalculationStatusViewModel();
      }

      protected override async Task OnInitializedAsync()
      {
         await LoadCalculations();
      }

      private async Task LoadCalculations()
      {
         CalculationJobs = await CalculatorService.GetCalculations();
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

                  // sleep (1000); ?

                  // OR use timer !!?

               } while (statusObject.Status == Models.CalculationStatus.Running);

               //if (statusObject.Status == Models.CalculationStatus.Completed)
               //{
               //   //Hidden = "visibility: visible";
               //}
            });
         }
         finally
         {
            IsCalculating = false;

            StateHasChanged();
         }
      }



      //protected string FormIsInvalid { get; set; } = "disabled";


      //[Parameter]
      //public EventCallback<CalculationRequest> CalculationRequestChanged { get; set; }

      //private InputWatcher inputWatcher;
      //private bool isInvalid = false;


      //public InputWatcher InputWatcher { get; set; }

      //private InputWatcher inputWatcher;

      //private bool isInvalid = false;
      //private void FieldChanged(string fieldName)
      //{
      //   //Console.WriteLine($"*** {Customer.Name}");
      //   isInvalid = !inputWatcher.Validate();
      //   CustomerChanged.InvokeAsync(Customer);
      //}

   }
}
