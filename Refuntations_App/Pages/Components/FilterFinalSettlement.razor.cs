using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Refuntations_App.Shared;
using Refuntations_App_Data.CustomModel;
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
        public List<string?> Status = new List<string?>();
       
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
                partnerName = displayFinalSettlements.Select(x => x.Dobavljac).Distinct().OrderBy(x => x).ToList();
                category = displayFinalSettlements.Select(x => x.Kategorija).Distinct().OrderBy(x => x).ToList();
                codeAA = displayFinalSettlements.Select(x => x.Sifra_AA).Distinct().OrderBy(x => x.Value).ToList();
                periodOfAA = displayFinalSettlements.Select(x => x.datum_od_aa).Distinct().OrderBy(x => x.Value).ToList();
                periodToAA = displayFinalSettlements.Select(x => x.datum_do_aa).Distinct().OrderBy(x => x.Value).ToList();
                Status = displayFinalSettlements.Select(x => x.status_stavke_obracuna).Distinct().OrderBy(x => x).ToList();
            }
        }
        public async Task ApplyFilter(FilterModel? filterModel)
        {
            finalSettlements = displayFinalSettlements;
            finalSettlements = finalSettlements.Where(x => x.Dobavljac == (filterModel.partnerName != null ? filterModel.partnerName : x.Dobavljac)
            && x.Kategorija == (filterModel.categoryName != null ? filterModel.categoryName : x.Kategorija)
            && x.Sifra_AA == (filterModel.codeAa != null ? filterModel.codeAa : x.Sifra_AA)
            && x.datum_od_aa == (filterModel.periodOf != null ? filterModel.periodOf : x.datum_od_aa)
            && x.status_stavke_obracuna == (filterModel.Status != null ? filterModel.Status : x.status_stavke_obracuna)
            && x.ir_stopa_1 == (filterModel.PDV10 == true ? true : (filterModel.PDV20 == true ? false : x.ir_stopa_1))
            && x.ir_stopa_2 == (filterModel.PDV20 == true ? true : (filterModel.PDV10 == true ? false : x.ir_stopa_2))
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
