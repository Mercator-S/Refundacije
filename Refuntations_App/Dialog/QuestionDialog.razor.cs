using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.CustomModel;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Dialog
{
    partial class QuestionDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        [Inject]
        private IProcedureExecutor? procedureExecutor { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Parameter]
        public int function { get; set; }
        [Parameter]
        public string dialogText { get; set; }
        [Parameter]
        public YearAndMonth yearAndMonth { get; set; }
        private async Task Submit()
        {
            List<FinalSettlementsViewModel> FinalSettlements = new List<FinalSettlementsViewModel>();
            switch (function)
            {
                case 4:
                    FinalSettlements = await Task.Run(() => procedureExecutor.AcceptanceSettlement(finalSettlements,yearAndMonth));
                    break;
                case 5:
                    FinalSettlements = await Task.Run(() => procedureExecutor.CancellationOfSettlement(finalSettlements, yearAndMonth));
                    break;
                default:
                    break;
            }
            DialogParameters parameteres = new DialogParameters
            {
                {"finalSettlements", FinalSettlements }
            };
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
