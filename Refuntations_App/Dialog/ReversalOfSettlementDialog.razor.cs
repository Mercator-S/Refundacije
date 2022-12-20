using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Repositories;
using Refundation_App_Services.Services;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Dialog
{
    partial class ReversalOfSettlementDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Inject]
        IProcedureExecutor procedureExecutor { get; set; }
        DateTime? date = DateTime.Today;
        private async Task Submit()
        {
            var result = await procedureExecutor.ReversalOfSettlementDialog(date, finalSettlements);
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
