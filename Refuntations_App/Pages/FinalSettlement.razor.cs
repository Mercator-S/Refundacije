using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App.Shared;
using Refuntations_App_Data.Model;
using System.Linq;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        public List<FinalSettlements> finalSettlements { get; set; }
        public List<FinalSettlements> displayFinalSettlements { get; set; }
        public FilterModel? filterModel = new FilterModel();
        [CascadingParameter]
        public EventCallback closeNavMenu { get; set; }
        public string TextValue { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public List<string?> partnerName = new List<string?>();
        public List<string?> category = new List<string?>();
        public List<int?> codeAA = new List<int?>();
        public List<DateTime?> periodOfAA = new List<DateTime?>();
        public List<DateTime?> periodToAA = new List<DateTime?>();
        public List<short?> Status= new List<short?>();

        public bool Hide { get; set; } = true;

        public async Task ShowDialog()
        {
            DialogResult result = await DialogService.Show<FinalSettlementDialog>("Kreiranje obracuna refundacija", dialogOptions).Result;
            DialogParameters ReturnParameteres = (DialogParameters)result.Data;
            if (ReturnParameteres != null)
            {
                var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                finalSettlements = (List<FinalSettlements>?)dialogResult[0];
                displayFinalSettlements = finalSettlements;
            }
        }
        public async Task ShowFilter()
        {
            if (displayFinalSettlements == null)
            {
                displayFinalSettlements = finalSettlements;
            }
            await closeNavMenu.InvokeAsync();
            Hide = false;
            partnerName = finalSettlements.Select(x => x.Dobavljac).Distinct().OrderBy(x => x).ToList();
            category = finalSettlements.Select(x => x.Kategorija).Distinct().OrderBy(x => x).ToList();
            codeAA = finalSettlements.Select(x => x.Sifra_AA).Distinct().OrderBy(x => x.Value).ToList();
            periodOfAA = finalSettlements.Select(x => x.datum_od_aa).Distinct().OrderBy(x => x.Value).ToList();
            periodToAA = finalSettlements.Select(x => x.datum_do_aa).Distinct().OrderBy(x => x.Value).ToList();
            Status= finalSettlements.Select(x => x.status_stavke_obracuna).Distinct().OrderBy(x => x.Value).ToList();

        }
        public async Task ApplyFilter(FilterModel? filterModel)
        {
            finalSettlements = finalSettlements.Where(x => x.Dobavljac == (filterModel.partnerName != null ? filterModel.partnerName : x.Dobavljac)
            && x.Kategorija == (filterModel.categoryName != null ? filterModel.categoryName : x.Kategorija)
            && x.Sifra_AA == (filterModel.codeAa != null ? filterModel.codeAa : x.Sifra_AA)
            && x.datum_od_aa == (filterModel.periodOf != null ? filterModel.periodOf : x.datum_od_aa)
            && x.datum_do_aa == (filterModel.periodTo != null ? filterModel.periodTo : x.datum_do_aa)).ToList();
        }
        public async Task ResetFilter()
        {
            finalSettlements = displayFinalSettlements;
            filterModel = new FilterModel();
        }
        public async Task CloseFilter()
        {
            await closeNavMenu.InvokeAsync();
            Hide = true;
        }
    }
}
