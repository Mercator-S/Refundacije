using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Services
{
    public interface IFileLoader
    {
        public List<InternalSupplier> loadInternalSuppliersFromExcel(FileInfo fileInfo);
        public List<ForeignSupplier> loadForeignSuppliersFromExcel(FileInfo fileInfo);
        public List<AAPdvSAPKeyMaterial> loadActitiviesWithPDVAndSAPKeyAndMaterialFromExcel(FileInfo fileInfo);
        public List<CategoryInternalOrderCostLocation> loadCategoryInternalOrderAndCostLocationFromExcel(FileInfo fileInto);
        public List<CounterSapIdSapKeyAmount> loadCounterSAPIdAndAmountFromExcel(FileInfo fileInto);
        List<Email> loadEmailsFromExcel(FileInfo fileInfo);
    }
}
