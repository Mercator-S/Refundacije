using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App.Dialog
{
    partial class ChangePartnerDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        [Inject]
        ISnackbar Snackbar { get; set; } = default!;
        public List<Partner> partnerList { get; set; }
        [Parameter]
        public List<FinalSettlementsViewModel> finalSettlements { get; set; }
        [Inject]
        IProcedureExecutor procedureExecutor { get; set; }
        private string searchString;
        protected override async Task OnInitializedAsync()
        {
            partnerList = await procedureExecutor.GetPartner(finalSettlements.Select(x => x.datum_do_aa.Value.Year).First(), finalSettlements.Select(x => x.datum_do_aa.Value.Month).First());
        }
        public async Task Submit()
        {
            var result = await procedureExecutor.ChangePartner(finalSettlements, partnerList.Where(x => x.naziv_partnera == searchString).Select(x => x.sap_sifra_dob).First().ToString());
            if (result != null)
            {
                DialogParameters parameteres = new DialogParameters
                {
                    { "finalSettlements", result }
                };
                MudDialog.Close(DialogResult.Ok(parameteres));
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;
                Snackbar.Configuration.VisibleStateDuration = 3000;
                Snackbar.Add($"Geška prilikom izmene partnera", Severity.Error);
            }
        }
        private async Task<IEnumerable<string>> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return partnerList.Select(x => x.naziv_partnera).ToList();
            return partnerList.Where(x => x.naziv_partnera.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.naziv_partnera).ToList();
        }
        void Cancel() => MudDialog.Cancel();
    }
}
