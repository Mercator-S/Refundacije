using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        private FilterFinalSettlement? filter;
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public bool Hide { get; set; } = true;
        public bool managementSettlements { get; set; } = true;

        public async Task ShowDialog()
        {
            DialogResult result = await DialogService.Show<FinalSettlementDialog>("Kreiranje obracuna refundacija", dialogOptions).Result;
            DialogParameters ReturnParameteres = (DialogParameters)result.Data;
            if (ReturnParameteres != null)
            {
                var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
            }
        }
        public async Task CheckedCheckBox(FinalSettlementsViewModel finalSettlements)
        {
            List<FinalSettlementsViewModel> finalSettlementsList = new List<FinalSettlementsViewModel>();
            finalSettlementsList.Add(finalSettlements);
            managementSettlements = !managementSettlements;
        }
        public async Task ManagementSettlements()
        {
            managementSettlements = !managementSettlements;
        }
        public async Task ShowFilter()
        {
            await filter.ShowFilter();
        }
    }
}
