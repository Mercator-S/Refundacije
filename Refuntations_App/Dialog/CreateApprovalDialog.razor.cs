using Microsoft.AspNetCore.Components;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Dialog
{
    partial class CreateApprovalDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }
        [Inject]
        private IProcedureExecutor? procedureExecutor { get; set; }
        public List<Approvals> Approvals { get; set; }
        public DateTime? documentDate { get; set; }
        public DateTime? dateOfReceipt { get; set; }
        public async Task GetNewApprovals(DateTime? documentDate, DateTime? dateOfReceipt)
        {
            Approvals = await procedureExecutor.GetNewApprovals(documentDate, dateOfReceipt);
        }
        void Cancel()
        {
            MudDialog.Close();
        }
    }
}
