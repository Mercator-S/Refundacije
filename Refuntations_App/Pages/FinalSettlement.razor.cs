using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        private FilterFinalSettlement? filter;
        public List<FinalSettlementsViewModel> finalSettlementsList = new List<FinalSettlementsViewModel>();
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public bool Hide { get; set; } = true;
        public bool managementSettlements { get; set; } = true;

        public async Task ShowDialog()
        {
            DialogParameters parameteres = new DialogParameters
            {
                { "finalSettlements", finalSettlements }
            };
            DialogResult result = await DialogService.Show<FinalSettlementDialog>("Konačni obračun", parameteres, dialogOptions).Result;
            DialogParameters ReturnParameteres = (DialogParameters)result.Data;
            if (ReturnParameteres != null)
            {
                var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
            }
        }
        public async Task CheckedCheckBox(FinalSettlementsViewModel FinalSettlements)
        {
            if (!FinalSettlements.Checked)
            {
                FinalSettlements.Checked = true;
                finalSettlementsList.Add(FinalSettlements);
            }
            else if (FinalSettlements.Checked)
            {
                FinalSettlements.Checked = false;
                finalSettlementsList.Remove(FinalSettlements);
            }
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
