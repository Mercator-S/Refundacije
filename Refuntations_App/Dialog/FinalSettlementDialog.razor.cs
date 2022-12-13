using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;
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
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month - 1;
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.TopCenter, DisableBackdropClick = true };
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }


        private async Task Submit(int Year, int Month)
        {
            DialogParameters parameteres = new DialogParameters();
            var result = await Task.Run(() => procedureExecutor.CheckFinalSettlement(Year, Month));
            if (result)
            {
                var finalSettlements = await Task.Run(() => procedureExecutor.GetFinalSettlement(Year, Month));
                parameteres.Add("finalSettlements", finalSettlements);
                MudDialog.Close(DialogResult.Ok(parameteres));
            }
            else
            {
                parameteres.Add("finalSettlements", await CreateFinalSettlement(Year, Month));
                MudDialog.Close(DialogResult.Ok(parameteres));
            }
        }

        void Cancel()
        {
            MudDialog.Close();
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
