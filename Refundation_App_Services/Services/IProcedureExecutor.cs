using Refuntations_App_Data.CustomModel;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month);
        Task<bool> CheckFinalSettlement(int Year, int Month);
        Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month);
        Task HandleNewEmailsAdded();
        Task<List<FinalSettlementsViewModel>> ChangePartner(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth, string sap_id);
        List<Partner> GetPartner(int Year, int Month);
        Task<List<FinalSettlementsViewModel>> ReversalOfSettlementDialog(DateTime? date, YearAndMonth yearAndMonth, List<FinalSettlementsViewModel> finalSettlements);
        Task<List<FinalSettlementsViewModel>> AcceptanceSettlement(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth);
        Task<List<FinalSettlementsViewModel>> CancellationOfSettlement(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth);
        Task<List<Approvals>> GetApprovals(long status);
        Task<List<Approvals>> GetNewApprovals(DateTime? documentDate, DateTime? dateOfReceipt);
        Task<List<ApprovalStatus>> GetApprovalStatus();
        int GetAlternativeSupplierFailures(int year, int month);
        void ExportFinalCalculation(int year, int month);
        int GetCalculationStatus(int Year, int Month);
        void AcceptSettements(string itemsId);
    }
}
