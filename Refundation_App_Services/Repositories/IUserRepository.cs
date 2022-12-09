using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Repositories
{
    public interface IUserRepository
    {
        public Task<OnlineUser> GetLoggedUser();
    }
}
