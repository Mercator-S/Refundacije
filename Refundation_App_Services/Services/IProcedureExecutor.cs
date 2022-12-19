using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month);
        Task<bool> CheckFinalSettlement(int Year, int Month);
        Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month);
        public Task HandleNewEmailsAdded();
        int GetAlternativeSupplierFailures(int year, int month);
        void ExportFinalCalculation(int year, int month);
        int GetCalculationStatus(int Year, int Month);
    }
}
