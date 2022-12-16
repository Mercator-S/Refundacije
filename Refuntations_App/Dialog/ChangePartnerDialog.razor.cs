using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refuntations_App_Data.CustomDataModel;
using Refuntations_App_Data.ViewModel;
using static MudBlazor.Colors;

namespace Refuntations_App.Dialog
{
    partial class ChangePartnerDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public List<Partner> partnerList { get; set; }
        [Parameter]
        public string partner { get; set; }
        private string searchString;
        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
        private async Task<IEnumerable<string>> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return partnerList.Select(x => x.partner_Name).ToList();
            return partnerList.Where(x => x.partner_Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.partner_Name).ToList();
        }
    }
}
