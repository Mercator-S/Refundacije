using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Repositories
{
    public class ProcedureExecutor : IProcedureExecutor
    {
        private IUserRepository userRepository { get; set; }
        
        private ApplicationDbContext _context { get; set; }
        public ProcedureExecutor(ApplicationDbContext contextFactory, IUserRepository userRepository)
        {
            _context = contextFactory;
            this.userRepository= userRepository;
        }
        public async Task<List<FinalSettlements>> GetFinalSettlement(int Year,int Month)
        {
            try {
                var res = _context.finalSettlement.FromSqlRaw("EXECUTE  usp_refundacije_prikaz_konacni_obracun {0},{1}", Year, Month).ToList();
                return res;
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public Task HandleNewEmailsAdded()
        {
            OnlineUser loggedUser = userRepository.GetLoggedUser().Result;
            _context.Database.ExecuteSqlRaw("EXEC usp_import_mailova {0}", loggedUser.UserName);
            return null;
        }
    }
}
