using Microsoft.AspNetCore.Components;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages.Components
{
    partial class FilterFinalSettlement
    {
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Parameter]
        public EventCallback<List<FinalSettlementsViewModel>> finalSettlementsChanged { get; set; }
        [Parameter]
        public bool Hide { get; set; }
        [Parameter]
        public EventCallback<bool> HideChanged { get; set; }
        [CascadingParameter]
        public EventCallback closeNavMenu { get; set; }
        public List<FinalSettlementsViewModel> displayFinalSettlements { get; set; }
        public FilterModel? filterModel = new FilterModel();
        public List<string?> partnerName = new List<string?>();
        public List<string?> category = new List<string?>();
        public List<int?> codeAA = new List<int?>();
        public List<DateTime?> periodOfAA = new List<DateTime?>();
        public List<DateTime?> periodToAA = new List<DateTime?>();
        public List<short?> Status = new List<short?>();

        public async Task ShowFilter()
        {
            if (displayFinalSettlements == null)
            {
                displayFinalSettlements = finalSettlements;
            }
            if (finalSettlements != null)
            {
                Hide = !Hide;
                await closeNavMenu.InvokeAsync();
                await HideChanged.InvokeAsync(Hide);
                partnerName = finalSettlements.Select(x => x.Dobavljac).Distinct().OrderBy(x => x).ToList();
                category = finalSettlements.Select(x => x.Kategorija).Distinct().OrderBy(x => x).ToList();
                codeAA = finalSettlements.Select(x => x.Sifra_AA).Distinct().OrderBy(x => x.Value).ToList();
                periodOfAA = finalSettlements.Select(x => x.datum_od_aa).Distinct().OrderBy(x => x.Value).ToList();
                periodToAA = finalSettlements.Select(x => x.datum_do_aa).Distinct().OrderBy(x => x.Value).ToList();
                Status = finalSettlements.Select(x => x.status_stavke_obracuna).Distinct().OrderBy(x => x.Value).ToList();
            }
        }
        public async Task ApplyFilter(FilterModel? filterModel)
        {
            finalSettlements = finalSettlements.Where(x => x.Dobavljac == (filterModel.partnerName != null ? filterModel.partnerName : x.Dobavljac)
            && x.Kategorija == (filterModel.categoryName != null ? filterModel.categoryName : x.Kategorija)
            && x.Sifra_AA == (filterModel.codeAa != null ? filterModel.codeAa : x.Sifra_AA)
            && x.datum_od_aa == (filterModel.periodOf != null ? filterModel.periodOf : x.datum_od_aa)
            && x.datum_do_aa == (filterModel.periodTo != null ? filterModel.periodTo : x.datum_do_aa)).ToList();
            await finalSettlementsChanged.InvokeAsync(finalSettlements);
        }
        public async Task ResetFilter()
        {
            finalSettlements = displayFinalSettlements;
            filterModel = new FilterModel();
            await finalSettlementsChanged.InvokeAsync(finalSettlements);
        }
        public async Task CloseFilter()
        {
            Hide = true;
            await closeNavMenu.InvokeAsync();
            await HideChanged.InvokeAsync(Hide);
        }
    }
}
