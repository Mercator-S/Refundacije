using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;

namespace Refundation_App_Services.Repositories
{
    public class ProcedureExecutor : IProcedureExecutor
    {
        private readonly IMapper _mapper;
        private ApplicationDbContext _context { get; set; }
        private IUserRepository userRepository { get; set; }
        public ProcedureExecutor(ApplicationDbContext contextFactory, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _context = contextFactory;
            this.userRepository= userRepository;
        }
        public async Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month)
        {
            await CheckFinalSettlement(Year, Month);
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
            string commaneed = "select count(*) from [Refundacije].[dbo].[tab_refundacije_konacni_obracun_zaglavlje] where active = 1 and godina = 2022 and mesec = 11";
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.Connection.Open();
                cmd.CommandType=System.Data.CommandType.Text;
                cmd.CommandText= commaneed;
                var a= cmd.ExecuteScalar();
            }
            
            //var k = (int)parameterReturn.Value;

            return true;
        }

        public Task HandleNewEmailsAdded()
        {
            OnlineUser loggedUser = userRepository.GetLoggedUser().Result;
            _context.Database.ExecuteSqlRaw("EXEC usp_import_mailova {0}", loggedUser.UserName);
            return null;
        }
    }
}
