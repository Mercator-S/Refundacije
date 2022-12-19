using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Services
{
    public interface IUserRepository
    {
        public Task<OnlineUser> GetLoggedUser();
    }
}
