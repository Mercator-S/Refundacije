using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month);
        Task<bool> CheckFinalSettlement(int Year, int Month);
        Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month);
        public Task HandleNewEmailsAdded();
        Task<List<FinalSettlementsViewModel>> ChangePartner(List<FinalSettlementsViewModel> finalSettlements, string sap_id);
        Task<List<Partner>> GetPartner(int Year, int Month);
    }
}
