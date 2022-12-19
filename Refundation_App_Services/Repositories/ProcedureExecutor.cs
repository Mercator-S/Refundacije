using AutoMapper;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;
using Refuntations_App_Data.ViewModel;
using System.Data;

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
            this.userRepository = userRepository;
        }
        public async Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Year, int Month)
        {
            return _mapper.Map<List<FinalSettlementsViewModel>>(_context.finalSettlement.FromSqlRaw("EXEC  usp_refundacije_prikaz_konacni_obracun {0},{1}", Year, Month).ToList());
        }
        public async Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month)
        {
            var godina = new SqlParameter("@godina", Year);
            var mesec = new SqlParameter("@mesec", Month);
            var korisnik = new SqlParameter("@korisnik", "administrator");

            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            _context.Database.ExecuteSqlRaw("exec @returnValue = usp_refundacije_konacni_obracun @godina, @mesec, @korisnik ", parameterReturn, godina, mesec, korisnik);
            int returnValue = (int)parameterReturn.Value;
            if (returnValue == 1)
                return _mapper.Map<List<FinalSettlementsViewModel>>(_context.finalSettlement.FromSqlRaw("EXEC  usp_refundacije_prikaz_konacni_obracun {0},{1}", Year, Month).ToList());
            else
                return new List<FinalSettlementsViewModel>();
        }
        public async Task<bool> CheckFinalSettlement(int Year, int Month)
        {
            var checkResult = _context.finalSettlementHeader.
                FromSqlRaw($"select id from tab_refundacije_konacni_obracun_zaglavlje where active = 1 and godina={Year} and mesec={Month}").Count();
            if (checkResult != 0)
                return true;
            return false;
        }
        public Task HandleNewEmailsAdded()
        {
            OnlineUser loggedUser = userRepository.GetLoggedUser().Result;
            _context.Database.ExecuteSqlRaw("EXEC usp_import_mailova {0}", loggedUser.UserName);
            return null;
        }

        public int GetAlternativeSupplierFailures(int year, int month)
        {
            try
            {
                using (var sqlCon = new SqlConnection(
    "Server=10.0.101.12;Database=Refundacije;Trusted_Connection=True;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;"))
                {
                    sqlCon.Open();
                    int result = Int32.Parse(new SqlCommand("EXEC usp_refundacije_konacni_obracun_provera_alternativnih_dobavljaca " + year.ToString() + "," + month.ToString() + ";",
                        sqlCon).ExecuteScalar().ToString());
                    sqlCon.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public void ExportFinalCalculation(int year, int month)
        {
            try {
                OnlineUser loggedUser = userRepository.GetLoggedUser().Result;
                _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_eksport_konacnog_obracuna {0},{1},{2}", year, month, loggedUser.UserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetCalculationStatus(int Year, int Month)
        {
            List<FinalSettlementHeader> headers = _context.finalSettlementHeader.Where(x =>x.Active == true && x.Godina==Year && x.Mesec==Month).ToList();
            return headers.ElementAt(0).Status;
        }
        

        
    }
}
