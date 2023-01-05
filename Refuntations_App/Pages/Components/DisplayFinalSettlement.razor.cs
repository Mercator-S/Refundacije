using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App.Dialog;
using Refuntations_App_Data.CustomModel;
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
        [Parameter]
        public EventCallback<YearAndMonth> SetYearAndMonth { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.TopCenter, DisableBackdropClick = true };
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month;
        public bool _processing { get; set; } = false;
        public async Task GetFinalSettlements(int Year, int Month)
        {
            _processing= true;
            var result = await Task.Run(() => procedureExecutor.CheckFinalSettlement(Year, Month));
            if (result)
            {
                await finalSettlementsChanged.InvokeAsync(await procedureExecutor.GetFinalSettlement(Year, Month));
                await SetYearAndMonth.InvokeAsync(new YearAndMonth(Year,Month));
            }
            else
            {
                await finalSettlementsChanged.InvokeAsync(await CreateFinalSettlement(Year, Month));
                await SetYearAndMonth.InvokeAsync(new YearAndMonth(Year, Month));
            }
            _processing = false;
        }
        public async Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month)
        {
            DialogParameters parameteres = new DialogParameters
            {
                { "Year", Year },
                { "Month", Month }
            };
            DialogResult result = await DialogService.Show<QuestionForCreatingFs>("Kreiraj konačni obračun", parameteres, dialogOptions).Result;
            DialogParameters ReturnParameteres = (DialogParameters)result.Data;
            if (ReturnParameteres != null)
            {
                var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
            }
            return finalSettlements;
        }
    }
}
