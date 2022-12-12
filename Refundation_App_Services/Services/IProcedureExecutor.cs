using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IProcedureExecutor
    {
        Task<List<Fin   public Task HandleNewEmailsAdded();alSettlements>> GetFinalSettlement(int Year, int Month);
        public Task HandleNewEmailsAdded();
    }
}
