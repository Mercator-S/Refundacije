using Microsoft.AspNetCore.Components;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Pages.Components
{
    partial class DisplayApprovals
    {
        [Inject]
        public IProcedureExecutor procedureExecutor { get; set; } = default!;
        [Parameter]
        public EventCallback<List<Approvals>> getApprovals { get; set; }
        [Parameter]
        public List<Approvals> approvals { get; set; }
        public List<ApprovalStatus>? approvalStatuses { get; set; }
        public long statusId { get; set; } = 1;
        public bool _processing { get; set; }
        protected override async Task OnInitializedAsync()
        {
            approvalStatuses = await procedureExecutor.GetApprovalStatus();
        }
        public async Task GetApprovals(long statusId)
        {
            await getApprovals.InvokeAsync(await procedureExecutor.GetApprovals(statusId));
        }
    }
}
