using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Refundation_App_Services.Services;
using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<OnlineUser> _UserManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public UserRepository(UserManager<OnlineUser> userManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _UserManager = userManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<OnlineUser> GetLoggedUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return await Task.FromResult(_UserManager.GetUserAsync(authState.User).Result);
        }
    }
}
