using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Services
{
    public interface IRefundationRepository
    {
        Task<string> MakeIdFromList(List<FinalSettlementsViewModel> finalSettlements);
    }
}
