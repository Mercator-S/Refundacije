using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;

namespace Refuntations_App.Dialog
{
    partial class FinalSettlementDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Inject]
        private IProcedureExecutor? procedureExecutor { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month - 1;

        private async Task Submit(int Year, int Month)
        {
            DialogParameters parameteres = new DialogParameters();
            var finalSettlements = await Task.Run(() => procedureExecutor.GetFinalSettlement(Year, Month));
            parameteres.Add("finalSettlements", finalSettlements);
            MudDialog.Close(DialogResult.Ok(parameteres));
        }

        void Cancel()
        {
            MudDialog.Close();
        }
    }
}
