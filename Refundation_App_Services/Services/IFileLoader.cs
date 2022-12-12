using Refuntations_App_Data.Model;


namespace Refundation_App_Services.Services
{
    public interface IFileLoader
    {
        public List<InternalSupplier> loadInternalSuppliersFromExcel(FileInfo fileInfo, out List<int> fails, out string error);
        public List<ForeignSupplier> loadForeignSuppliersFromExcel(FileInfo fileInfo, out List<int> fails, out string error);
        public List<AAPdvSAPKeyMaterial> loadActitiviesWithPDVAndSAPKeyAndMaterialFromExcel(FileInfo fileInfo, out List<int> fails, out string error);
        public List<CategoryInternalOrderCostLocation> loadCategoryInternalOrderAndCostLocationFromExcel(FileInfo fileInto, out List<int> fails, out string error);
        public List<CounterSapIdSapKeyAmount> loadCounterSAPIdAndAmountFromExcel(FileInfo fileInto, out List<int> fails, out string error);
        List<EmailImport> loadEmailsFromExcel(FileInfo fileInfo, out List<int> fails, out string error);
    }
}
