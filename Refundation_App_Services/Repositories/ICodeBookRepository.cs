using Microsoft.EntityFrameworkCore;
using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Repositories
{
    public interface ICodeBookRepository
    {
        public Task Add(List<object> entities, string type);
        public Task<IEnumerable<object>> Get(string type);
        public  Task<object> Delete(InternalSupplier entity);
        public  Task<object> Delete(ForeignSupplier entity);
        public  Task<object> Delete(AAPdvSAPKeyMaterial entity);
        public  Task<object> Delete(CounterSapIdSapKeyAmount entity);
        public  Task<object> Delete(CategoryInternalOrderCostLocation entity);
        public  Task<object> Delete(Email entity);
        public Task<List<object>> DeleteRange(List<InternalSupplier> entities, string targetGroup);
        public Task<List<object>> DeleteRange(List<ForeignSupplier> entities, string targetGroup);
        public Task<List<object>> DeleteRange(List<AAPdvSAPKeyMaterial> entities, string targetGroup);
        public Task<List<object>> DeleteRange(List<CounterSapIdSapKeyAmount> entities, string targetGroup);
        public Task<List<object>> DeleteRange(List<CategoryInternalOrderCostLocation> entities, string targetGroup);
        public Task<List<object>> DeleteRange(List<Email> entities, string targetGroup);
    }
}
