using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App.Dialog;
using Refuntations_App.Pages.Components;
using Refuntations_App_Data.CustomModel;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Pages
{
    partial class FinalSettlement
    {
        [Inject]
        ISnackbar Snackbar { get; set; } = default!;
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        private FilterFinalSettlement? filter;
        public List<FinalSettlementsViewModel> finalSettlementsList = new List<FinalSettlementsViewModel>();
        [Inject]
        private IProcedureExecutor _procedureExecutor { get; set; }
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public bool Hide { get; set; } = true;
        public bool managementSettlements { get; set; } = true;
        public YearAndMonth yearAndMonth { get; set; }
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
                var dialogResult = ReturnParameteres.Select(FinallSettlement => FinallSettlement.Value).ToList();
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
        public void ManagementSettlements()
        {
            managementSettlements = !managementSettlements;
        }
        public async Task ShowFilter()
        {
            await filter.ShowFilter();
        }
        public async Task ChangePartner()
        {
            if (finalSettlementsList.Select(x => x.Dobavljac).Distinct().Count() == 1)
            {
                DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", finalSettlementsList },
                    {"yearAndMonth",yearAndMonth }
                };
                DialogResult result = await DialogService.Show<ChangePartnerDialog>("Izmena dobavljača", parameteres, dialogOptions).Result;
                DialogParameters ReturnParameteres = (DialogParameters)result.Data;
                if (ReturnParameteres != null)
                {
                    var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                    finalSettlementsList = new List<FinalSettlementsViewModel>();
                    finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
                }
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Morate izabrati terecenja sa istim dobavljačem", Severity.Error);
            }
        }
        public async Task ReversalOfSettlement()
        {
            if (finalSettlementsList.Select(x => x.status_stavke_obracuna).All(x => x == "Fakturisan") && finalSettlementsList.Select(x => x.status_stavke_obracuna).Distinct().Count() >= 1)
            {
                DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", finalSettlementsList },
                    {"yearAndMonth",yearAndMonth }
                };
                DialogResult result = await DialogService.Show<ReversalOfSettlementDialog>("Storniranje terecenja", parameteres, dialogOptions).Result;
                DialogParameters ReturnParameteres = (DialogParameters)result.Data;
                if (ReturnParameteres != null)
                {
                    var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                    finalSettlementsList = new List<FinalSettlementsViewModel>();
                    finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
                }
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Morate izabrati terećenja sa statusom \"U Obradi\" i morate izabrati minimum jedno terećenje.", Severity.Error);
            }
        }
        public async Task AcceptanceSettlement()
        {
            if (finalSettlementsList.Select(x => x.status_stavke_obracuna).All(x => x == "Kreiran" || x == "Eksportovan") && finalSettlementsList.Select(x => x.status_stavke_obracuna).Distinct().Count() >= 1)
            {
                string dialogText = "Da li želite da prihvatite odabrana terecenja?";
                int function = 4;
                DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", finalSettlementsList },
                    { "dialogText", dialogText },
                    { "function", function },
                     {"yearAndMonth",yearAndMonth }
                };
                DialogResult result = await DialogService.Show<QuestionDialog>("Prihvatanje terecenja", parameteres, dialogOptions).Result;
                DialogParameters ReturnParameteres = (DialogParameters)result.Data;
                if (ReturnParameteres != null)
                {
                    var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                    finalSettlementsList = new List<FinalSettlementsViewModel>();
                    finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
                }
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Morate izabrati terećenja sa statusom \"Kreiran\" ili \"Eksportovan\" takodje morate izabrati minimum jedno terećenje.", Severity.Error);
            }
        }
        public async Task CancellationOfSettlement()
        {
            if (finalSettlementsList.Select(x => x.status_stavke_obracuna).All(x => x == "Kreiran") && finalSettlementsList.Select(x => x.status_stavke_obracuna).Distinct().Count() >= 1)
            {
                string dialogText = "Da li želite da poništite odabrana terecenja?";
                int function = 5;
                DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", finalSettlementsList },
                    { "dialogText", dialogText },
                    { "function", function }
                };
                DialogResult result = await DialogService.Show<QuestionDialog>("Ponistavanje terecenja", parameteres, dialogOptions).Result;
                DialogParameters ReturnParameteres = (DialogParameters)result.Data;
                if (ReturnParameteres != null)
                {
                    var dialogResult = ReturnParameteres.Select(ListOfNarudzbineReturn => ListOfNarudzbineReturn.Value).ToList();
                    finalSettlementsList = new List<FinalSettlementsViewModel>();
                    finalSettlements = (List<FinalSettlementsViewModel>)dialogResult[0];
                }
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Morate izabrati terećenja sa statusom \"Kreiran\" takodje morate izabrati minimum jedno terećenje.", Severity.Error);
            }
        }
        public async Task ExportCalculation()
        {
            if (!IsStatusCreated(finalSettlements.ElementAt(0).fk_obracun))
            {
                showWarningDialog("Upozorenje", "Možete eksportovati samo izveštaj koji je u statusu: ", " KREIRAN.", @Icons.Filled.Warning, Color.Warning);
            }
            else
            {
                var confirmation = await ConfirmDecisionAsync();
                if (confirmation)
                {
                    int alternativeFailureNumber = _procedureExecutor.GetAlternativeSupplierFailures(yearAndMonth.Year, yearAndMonth.Month);
                    if (alternativeFailureNumber > 0)
                    {
                        showWarningDialog("Upozorenje", "Postoje interni dobavljaci koji nisu prebaceni na alternativne dobavljace.", null, @Icons.Filled.Warning, Color.Warning);
                    }
                    else
                    {
                        try
                        {
                            _procedureExecutor.ExportFinalCalculation(yearAndMonth.Year, yearAndMonth.Month);
                            showWarningDialog("Obaveštenje", "Terecenja uspesno eksportovana!\nU toku je generisanje eksporta, kada ono zavrsi sa radom dobicete obavestenje na Vaš mail.", null, @Icons.Filled.CircleNotifications, Color.Success);
                        }
                        catch (Exception e)
                        {
                            showWarningDialog("Greška", "Greška prilikom eksportovanja terećenja. Pokušajte ponovo, ili kontaktirajte tim podrške.", null, @Icons.Filled.Error, Color.Error);
                        }
                    }
                }
            }
        }
        private async Task<bool> ConfirmDecisionAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Title", "Potvrda" },
                { "Content", "Da li ste sigurni da želite da eksportujete trebovanja dobavljačima?" }
            };
            var result = await DialogService.Show<ConfirmationDialog>("Potvrda", parameters, options).Result;
            if (result != null && !result.Cancelled) return true;
            return false;
        }
        private void showWarningDialog(string title, string content, string status, string icon, MudBlazor.Color color)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "WarningText", content },
                { "Status", status == null ? "" : status },
                { "Title", title },
                { "Icon", icon },
                { "Color", color }
            };
            DialogService.Show<WarningDialog>("Upozorenje", parameters, options);
        }
        private bool IsStatusCreated(int fk_obracun)
        {
            int status = _procedureExecutor.GetCalculationStatus(yearAndMonth.Year, yearAndMonth.Month);
            return status == 1;
        }
        public void setYearAndMonthValue(YearAndMonth values)
        {
            yearAndMonth = values;
        }
    }
}
