using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.CustomModel;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Dialog
{
    partial class ReversalOfSettlementDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Inject]
        IProcedureExecutor procedureExecutor { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Parameter]
        public YearAndMonth yearAndMonth{ get; set; }
        DateTime? date = DateTime.Today;
        private async Task Submit()
        {
            var result = await procedureExecutor.ReversalOfSettlementDialog(date, yearAndMonth, finalSettlements);
            DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", result }
                };
            MudDialog.Close(DialogResult.Ok(parameteres));
        }
        void Cancel()
        {
            MudDialog.Close();
        }
    }
}
