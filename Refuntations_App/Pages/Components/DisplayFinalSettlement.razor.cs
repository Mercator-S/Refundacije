using Microsoft.AspNetCore.Components;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages.Components
{
    partial class DisplayFinalSettlement
    {
        [Inject]
        private IProcedureExecutor procedureExecutor { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Parameter]
        public EventCallback<List<FinalSettlementsViewModel>> finalSettlementsChanged { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month - 1;
        public async Task GetFinalSettlements(int Year, int Month)
        {
            await finalSettlementsChanged.InvokeAsync(await procedureExecutor.GetFinalSettlement(Year, Month));
        }
    }
}
