using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        public List<FinalSettlements> finalSettlements { get; set; }
        public string TextValue { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public async Task ShowDialog()
        {
            DialogResult result = await DialogService.Show<FinalSettlementDialog>("Kreiranje obracuna refundacija", dialogOptions).Result;
            DialogParameters ReturnParameteres = (DialogParameters)result.Data;
            if (ReturnParameteres != null)
            {
                var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                finalSettlements = (List<FinalSettlements>?)dialogResult[0];
            }
        }
    }
}
