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
