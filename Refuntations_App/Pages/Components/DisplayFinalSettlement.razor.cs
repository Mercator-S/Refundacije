using Microsoft.AspNetCore.Components;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Pages.Components
{
    partial class DisplayFinalSettlement
    {
        [Inject]
        private IProcedureExecutor procedureExecutor { get; set; }
        [Parameter]
        public List<FinalSettlements> finalSettlements { get; set; }
        [Parameter]
        public EventCallback<List<FinalSettlements>> finalSettlementsChanged { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month - 1;
        public async Task GetFinalSettlements(int Year, int Month)
        {
            await finalSettlementsChanged.InvokeAsync(await procedureExecutor.GetFinalSettlement(Year, Month));
        }
    }
}
