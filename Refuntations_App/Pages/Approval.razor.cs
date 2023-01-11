using MudBlazor;
using Refuntations_App.Dialog;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Pages
{
    partial class Approval
    {
        DialogOptions dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, Position = DialogPosition.Center, DisableBackdropClick = true };
        public List<Approvals> approvals { get; set; }
        public bool managementApprovals { get; set; } = false;
        public void GetApprovals(List<Approvals> approvalsFromContext)
        {
            approvals = approvalsFromContext;
        }
        public void CreateApprovalDialog()
        {
             DialogService.Show<CreateApprovalDialog>("Unos odobrenja", dialogOptions);
        }
    }
}
