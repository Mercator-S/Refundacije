using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<FinalSettlements>> GetFinalSettlement(int Year, int Month);
        public Task HandleNewEmailsAdded();
    }
}
