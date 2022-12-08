using Microsoft.AspNetCore.Components;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        [Inject]
        private IProcedureExecutor procedureExecutor { get; set; }
        public List<Refuntations_App_Data.Model.FinalSettlement> finalSettlements;
        protected override async Task OnInitializedAsync()
        {
            finalSettlements = await procedureExecutor.GetFinalSettlement();
        }
    }
}
