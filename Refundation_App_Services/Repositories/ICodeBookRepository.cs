using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Repositories
{
    public interface ICodeBookRepository
    {
        public Task<IEnumerable<InternalSupplier>> GetInternalSuppliers();
        public Task<IEnumerable<ForeignSupplier>> GetForeignSuppliers();
        public Task<IEnumerable<AAPdvSAPKeyMaterial>> GetActivitiesWithSAPKeyAndMaterial();
        public Task<IEnumerable<CategoryInternalOrderCostLocation>> GetCategoriesWithInternalOrderAndCostLocation();
        public Task<IEnumerable<CounterSapIdSapKeyAmount>> GetCountersWithSAPIdAndAmount();
        public Task<InternalSupplier> DeleteInternalSupplier(int id);
        public Task<ForeignSupplier> DeleteForeignSupplier(int id);
        public Task<AAPdvSAPKeyMaterial> DeleteAAPdvSAPKeyMaterial(int id);
        public Task<CategoryInternalOrderCostLocation> DeleteCategoryInternalOrderCostLocation(int id);
        public Task<CounterSapIdSapKeyAmount> DeleteCounterSAPIdAmount(int id);
        public Task AddInternalSuppliers(List<InternalSupplier> suppliersList);
        public Task AddForeignSuppliers(List<ForeignSupplier> suppliersList);
        public Task AddAAPdvSAPKeyMaterials(List<AAPdvSAPKeyMaterial> entities);
        public Task AddCategoryInternalOrderCostLocation(List<CategoryInternalOrderCostLocation> entites);
        public Task AddCounterSAPIdAmount(List<CounterSapIdSapKeyAmount> entities);
        public Task<IEnumerable<Email>> GetEmails();
        public Task<Email> DeleteSuppliersEmail(int id);
        public Task AddEmails(List<EmailImport> mails);
    }
}
