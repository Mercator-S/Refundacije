using Microsoft.AspNetCore.Mvc;
using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Services
{
    public interface ICodeBookService
    {
        object DeleteEntity(object entity, string targetGroup);
        Task<List<object>> GetEntitiesAsync( string targetGroup);
    }
}
