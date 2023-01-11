using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.CustomModel;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Dialog
{
    partial class FinalSettlementDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Inject]
        private IProcedureExecutor? procedureExecutor { get; set; }
        [Parameter]
        public EventCallback<List<FinalSettlementsViewModel>> finalSettlementsChanged { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Parameter]
        public YearAndMonth yearAndMonth { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.TopCenter, DisableBackdropClick = true };
        private async Task Submit(YearAndMonth yearAndMonth)
        {
            DialogParameters parameteres = new DialogParameters();
            var result = await Task.Run(() => procedureExecutor.CheckFinalSettlement(yearAndMonth.Year, yearAndMonth.Month));
            if (result)
            {
                var finalSettlements = await Task.Run(() => procedureExecutor.GetFinalSettlement(yearAndMonth.Year, yearAndMonth.Month));
                parameteres.Add("finalSettlements", finalSettlements);
                MudDialog.Close(DialogResult.Ok(parameteres));
            }
            else
            {
                parameteres.Add("finalSettlements", await CreateFinalSettlement(yearAndMonth));
                MudDialog.Close(DialogResult.Ok(parameteres));
            }
        }
        void Cancel()
        {
            MudDialog.Close();
        }
        public async Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(YearAndMonth yearAndMonth)
        {
            DialogParameters parameteres = new DialogParameters
            {
                { "Year", yearAndMonth.Year },
                { "Month", yearAndMonth.Month },
                { "finalSettlements",finalSettlements}
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
