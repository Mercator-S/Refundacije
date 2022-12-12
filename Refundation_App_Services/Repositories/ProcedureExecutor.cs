using AutoMapper;
using Microsoft.Data.SqlClient;
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
            return _mapper.Map<List<FinalSettlementsViewModel>>(_context.finalSettlement.FromSqlRaw("EXECUTE  usp_refundacije_prikaz_konacni_obracun {0},{1}", Year, Month).ToList());
        }
        public async Task<bool> CheckFinalSettlement(int Year, int Month)
        {
            var godina = new SqlParameter("@godina", Year);
            var mesec = new SqlParameter("@mesec", Month);
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            var a = _context.Database.ExecuteSqlRaw("exec @returnValue = usp_provera_zaglavlja {0},{1}", parameterReturn,2022,11);
            var k = (int)parameterReturn.Value;

            return true;
        }
    }
}
