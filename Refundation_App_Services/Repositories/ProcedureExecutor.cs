﻿using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Repositories
{
    public class ProcedureExecutor : IProcedureExecutor
    {
        
        private ApplicationDbContext _context { get; set; }
        public ProcedureExecutor(ApplicationDbContext contextFactory)
        {
            _context = contextFactory;
        }
        public async Task<List<FinalSettlements>> GetFinalSettlement(int Year,int Month)
        {
            return _context.finalSettlement.FromSqlRaw("EXECUTE  usp_refundacije_Prikaz_Konacni_Obracun_v2 {0},{1}", Year, Month).ToList();
        }
    }
}
