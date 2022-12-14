using Refundation_App_Services.Repositories;
using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Services.Impl
{
    public class CodeBookService : ICodeBookService
    {
        private readonly ICodeBookRepository _codeBookRepository;
        public CodeBookService(ICodeBookRepository codeBookRepository)
        {
            _codeBookRepository = codeBookRepository;
        }

        public async Task<List<object>> DeleteEntities(List<object> entities, string targetGroup)
        {

            List<object> result = new List<object>();
            switch (targetGroup)
            {
                case "Interni dobavljači":
                    List<InternalSupplier> internalSuppliers = new List<InternalSupplier>();
                    foreach (object entity in entities)
                        internalSuppliers.Add((InternalSupplier)entity);
                       result =_codeBookRepository.DeleteRange(internalSuppliers, targetGroup).Result;
                    break;
                case "Inostrani dobavljači":
                    List<ForeignSupplier> foreignSuppliers = new List<ForeignSupplier>();
                    foreach (object entity in entities)
                        foreignSuppliers.Add((ForeignSupplier)entity);
                    result = _codeBookRepository.DeleteRange(foreignSuppliers, targetGroup).Result;
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    List<CategoryInternalOrderCostLocation> categories = new List<CategoryInternalOrderCostLocation>();
                    foreach (object entity in entities)
                        categories.Add((CategoryInternalOrderCostLocation)entity);
                    result = _codeBookRepository.DeleteRange(categories, targetGroup).Result;

                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    List<AAPdvSAPKeyMaterial> activities = new List<AAPdvSAPKeyMaterial>();
                    foreach (object entity in entities)
                        activities.Add((AAPdvSAPKeyMaterial)entity);
                    result = _codeBookRepository.DeleteRange(activities, targetGroup).Result;
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    List<CounterSapIdSapKeyAmount> counters = new List<CounterSapIdSapKeyAmount>();
                    foreach (object entity in entities)
                        counters.Add((CounterSapIdSapKeyAmount)entity);
                    result = _codeBookRepository.DeleteRange(counters, targetGroup).Result;
                    break;
                case "Dobavljači - Email adrese":
                    List<Email> forDelete = new List<Email>();
                    foreach (object entity in entities)
                        forDelete.Add((Email)entity);
                    result = _codeBookRepository.DeleteRange(forDelete, targetGroup).Result;
                    break;
            }
            return result;
          
        }

        public object DeleteEntity(object entity, string targetGroup)
        {
            object result=null;
            switch (targetGroup)
            {
                case "Interni dobavljači":
                    //  result=_codeBookRepository.DeleteInternalSupplier(((InternalSupplier)entity).id);
                    result = _codeBookRepository.Delete((InternalSupplier)entity);

                    break;
                case "Inostrani dobavljači":
                    result = _codeBookRepository.Delete((ForeignSupplier)entity);
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    result = _codeBookRepository.Delete((CategoryInternalOrderCostLocation)entity);

                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    result = _codeBookRepository.Delete((AAPdvSAPKeyMaterial)entity);
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    result = _codeBookRepository.Delete((CounterSapIdSapKeyAmount)entity);
                    break;
                case "Dobavljači - Email adrese":
                    result = _codeBookRepository.Delete((Email)entity);
                    break;

            }
            return result;
        }

        public async Task<List<object>> GetEntitiesAsync(string targetGroup)
        {
            return (await _codeBookRepository.Get(targetGroup)).ToList();
        }
    }
}
