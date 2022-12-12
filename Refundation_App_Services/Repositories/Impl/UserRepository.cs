using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Repositories.Impl
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
