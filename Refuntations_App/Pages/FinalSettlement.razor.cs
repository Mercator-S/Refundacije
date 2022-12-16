using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App_Data.CustomDataModel;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        [Inject]
        ISnackbar Snackbar { get; set; } = default!;
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
        public async Task ChangePartner()
        {
            if (finalSettlementsList.Select(x => x.Dobavljac).Distinct().Count() == 1)
            {
                List<Partner> partners = finalSettlements.DistinctBy(x => x.sifra_dob).Select(x => new Partner(x.sifra_dob, x.Dobavljac)).ToList();
                DialogParameters parameteres = new DialogParameters
            {
                { "partnerList", partners },
                { "partner", finalSettlementsList.Select(x=>x.Dobavljac).First() }

            };
                DialogService.Show<ChangePartnerDialog>("Izmena dobavljača", parameteres, dialogOptions);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Morate izabrati terecenja samo sa istim dobavljačem", Severity.Error);
            }
        }
    }
}
