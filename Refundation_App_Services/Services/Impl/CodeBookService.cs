using Refundation_App_Services.Repositories;
using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Refundation_App_Services.Services.Impl
{
    public class CodeBookService : ICodeBookService
    {
        private readonly ICodeBookRepository _codeBookRepository;
        public CodeBookService(ICodeBookRepository codeBookRepository)
        {
            _codeBookRepository = codeBookRepository;
        }

        public object DeleteEntity(object entity, string targetGroup)
        {
            object result=null;
            switch (targetGroup)
            {
                case "Interni dobavljači":
                    result=_codeBookRepository.DeleteInternalSupplier(((InternalSupplier)entity).id);
                    break;
                case "Inostrani dobavljači":
                    result = _codeBookRepository.DeleteForeignSupplier(((ForeignSupplier)entity).id);
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    result = _codeBookRepository.DeleteCategoryInternalOrderCostLocation(((CategoryInternalOrderCostLocation)entity).id);

                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    result = _codeBookRepository.DeleteAAPdvSAPKeyMaterial(((AAPdvSAPKeyMaterial)entity).id);
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    result = _codeBookRepository.DeleteCounterSAPIdAmount(((CounterSapIdSapKeyAmount)entity).id);
                    break;

            }
            return result;
        }

        public async Task<List<object>> GetEntitiesAsync(string targetGroup)
        {
            List<object> Elements = new List<object>();
            switch (targetGroup)
            {
                case "Interni dobavljači":
                    List<InternalSupplier> dbInternal=(await _codeBookRepository.GetInternalSuppliers()).ToList();
                    Elements = new List<object>();
                    dbInternal.ForEach(p => Elements.Add(p));
                    break;

                case "Inostrani dobavljači":
                    List<ForeignSupplier> dbForeign = (await _codeBookRepository.GetForeignSuppliers()).ToList();
                    Elements = new List<object>();
                    dbForeign.ForEach(p => Elements.Add(p));
                    break;

                case "Kategorija - Interni nalog - mesto troška":
                    List<CategoryInternalOrderCostLocation> dbCategories = (await _codeBookRepository.GetCategoriesWithInternalOrderAndCostLocation()).ToList();
                    Elements = new List<object>();
                    dbCategories.ForEach(p => Elements.Add(p));
                    break;

                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    List<AAPdvSAPKeyMaterial> dbActitivies = (await _codeBookRepository.GetActivitiesWithSAPKeyAndMaterial()).ToList();
                    Elements = new List<object>();
                    dbActitivies.ForEach(p => Elements.Add(p));
                    break;

                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    List<CounterSapIdSapKeyAmount> dbACounter = (await _codeBookRepository.GetCountersWithSAPIdAndAmount()).ToList();
                    Elements = new List<object>();
                    dbACounter.ForEach(p => Elements.Add(p));
                    break;
            }
            return Elements;
        }
    }
}
