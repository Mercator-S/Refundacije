using AutoMapper;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refuntations_App_Data.AutoMapperProfile
{
    public class FinalSettlementsProfile : Profile
    {
        public FinalSettlementsProfile()
        {
            CreateMap<FinalSettlements, FinalSettlementsViewModel>();
        }
    }
}
