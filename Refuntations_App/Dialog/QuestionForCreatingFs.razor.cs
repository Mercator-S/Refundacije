using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.ViewModel;
using System.Reflection.Metadata;

namespace Refuntations_App.Dialog
{
    partial class QuestionForCreatingFs
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Inject]
        private IProcedureExecutor? procedureExecutor { get; set; }
        [Parameter]
        public int Year { get; set; } = DateTime.Now.Year;
        [Parameter]
        public int Month { get; set; } = DateTime.Now.Month - 1;
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        public bool _processing { get; set; } = false;

        private async Task Submit()
        {
            _processing = true;
            var finalSettlements = await Task.Run(() => procedureExecutor.CreateFinalSettlement(Year, Month));
            DialogParameters parameteres = new DialogParameters
            {
                {"finalSettlements", finalSettlements }
            };
            _processing = false;
            MudDialog.Close(DialogResult.Ok(parameteres));
        }
        void Cancel()
        {
            DialogParameters parameteres = new DialogParameters
            {
                { "finalSettlements", finalSettlements }
            };
            MudDialog.Close(DialogResult.Ok(parameteres));
        }
    }
}
