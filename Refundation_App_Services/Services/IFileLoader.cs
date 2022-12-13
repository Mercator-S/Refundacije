using Refuntations_App_Data.Model;


namespace Refundation_App_Services.Services
{
    public interface IFileLoader
    {
        public List<object> loadFromExcel(FileInfo fileInfo, out List<int> fails, out string error, Type type);
    }
}
