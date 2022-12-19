using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using MudBlazor;
using OfficeOpenXml.ConditionalFormatting;
using Refundation_App_Services.Repositories;
using Refundation_App_Services.Services;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        private FilterFinalSettlement? filter;
        public List<FinalSettlementsViewModel> finalSettlementsList = new List<FinalSettlementsViewModel>();
        [Inject]
        private IProcedureExecutor _procedureExecutor { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public bool Hide { get; set; } = true;
        public bool managementSettlements { get; set; } = true;
        private int Year { get; set; }
        private int Month { get; set; }


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
        public async Task ExportCalculation()
        {
            if (!IsStatusCreated(finalSettlements.ElementAt(0).fk_obracun))
            {
                showWarningDialog("Upozorenje", "Možete eksportovati samo izveštaj koji je u statusu: ", " KREIRAN.", @Icons.Filled.Warning, Color.Warning);
            }
            else
            {
                var confirmation=await ConfirmDecisionAsync();
                if (confirmation)
                {
                    int alternativeFailureNumber = _procedureExecutor.GetAlternativeSupplierFailures(finalSettlements.FirstOrDefault().Year, finalSettlements.FirstOrDefault().Month);
                    if (alternativeFailureNumber > 0)
                    {
                        showWarningDialog("Upozorenje", "Postoje interni dobavljaci koji nisu prebaceni na alternativne dobavljace.",null, @Icons.Filled.Warning, Color.Warning);
                    }
                    else
                    {
                        _procedureExecutor.ExportFinalCalculation(finalSettlements.FirstOrDefault().Year, finalSettlements.FirstOrDefault().Month);
                        showWarningDialog("Obaveštenje", "Terecenja uspesno eksportovana!\nU toku je generisanje eksporta, kada ono zavrsi sa radom dobicete obavestenje na Vaš mail.",null, @Icons.Filled.CircleNotifications, Color.Success);
                    }
                }
            }
        }

        private async Task<bool> ConfirmDecisionAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters();
            parameters.Add("Title", "Potvrda");
            parameters.Add("Content", "Da li ste sigurni da želite da eksportujete trebovanja dobavljačima?");
            var result=await DialogService.Show<ConfirmationDialog>("Potvrda", parameters, options).Result;
            if (result != null && !result.Cancelled) return true;
            return false;
            
        }

        private void showWarningDialog(string title, string content, string status, string icon, MudBlazor.Color color)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters();
            parameters.Add("WarningText", content);
            parameters.Add("Status", status==null ? "" : status);
            parameters.Add("Title", title);
            parameters.Add("Icon", icon);
            parameters.Add("Color", color);
            DialogService.Show<WarningDialog>("Upozorenje", parameters,options);
        }

        private bool IsStatusCreated(int fk_obracun)
        {
            int status= _procedureExecutor.GetCalculationStatus(Year, Month);
            return status == 1;
        }
        public async void setYearAndMonthValue(HashSet<int> values)
        {
            this.Year = values.ElementAt(0);
            this.Month = values.ElementAt(1);
        }
    }
}
