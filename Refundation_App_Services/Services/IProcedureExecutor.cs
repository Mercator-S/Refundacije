using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month);
        public Task HandleNewEmailsAdded();
    }
}
