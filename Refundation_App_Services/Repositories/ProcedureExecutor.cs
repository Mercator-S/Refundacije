using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;
using System.Collections.Generic;

namespace Refundation_App_Services.Repositories
{
    public class ProcedureExecutor : IProcedureExecutor
    {
        private readonly IMapper _mapper;
        private ApplicationDbContext _context { get; set; }
        public ProcedureExecutor(ApplicationDbContext contextFactory, IMapper mapper)
        {
            _mapper = mapper;
            _context = contextFactory;
        }
        public async Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month)
        {
            return _mapper.Map<List<FinalSettlementsViewModel>>(_context.finalSettlement.FromSqlRaw("EXECUTE  usp_refundacije_Prikaz_Konacni_Obracun_v2 {0},{1}", Year, Month).ToList());
        }
    }
}
