using Refundation_App_Services.Services;
using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Repositories
{
    public class RefundationRepository: IRefundationRepository
    {
        public async Task<string> MakeIdFromList(List<FinalSettlementsViewModel> finalSettlements)
        {
            string itemsId = "";
            foreach (var settlement in finalSettlements)
            {
                itemsId += settlement.id_iznos_stopa_1 != null ? settlement.id_iznos_stopa_1 : settlement.id_iznos_stopa_2;
                itemsId += ',';
            }
            itemsId = itemsId.Remove(itemsId.Length - 1);
            return itemsId;
        }
    }
}
