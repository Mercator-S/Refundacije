using AutoMapper;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.CustomModel;
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
        private IRefundationRepository _refundationRepository { get; set; }
        public ProcedureExecutor(ApplicationDbContext contextFactory, IMapper mapper, IUserRepository userRepository, IRefundationRepository refundationRepository)
        {
            _mapper = mapper;
            _context = contextFactory;
            this.userRepository = userRepository;
            _refundationRepository = refundationRepository;
        }
        public async Task<List<FinalSettlementsViewModel>> GetFinalSettlement(int Years, int Months)
        {
            try
            {
                return _mapper.Map<List<FinalSettlementsViewModel>>(_context.finalSettlement.FromSqlRaw("EXEC  usp_refundacije_prikaz_konacni_obracun {0},{1}", Years, Months).ToList());
            }
            catch (Exception E)
            {
                //?
                Console.WriteLine(E.Message);
                return new List<FinalSettlementsViewModel>();
            }

        }
        public async Task<List<FinalSettlementsViewModel>> CreateFinalSettlement(int Year, int Month)
        {
            var godina = new SqlParameter("@godina", Year);
            var mesec = new SqlParameter("@mesec", Month);
            var korisnik = new SqlParameter("@korisnik", "administrator");

            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
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
            //The return value cannot be null
            return null;
        }
        public async Task<List<FinalSettlementsViewModel>> ChangePartner(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth, string sap_id)
        {
            string itemsId = await _refundationRepository.MakeIdFromList(finalSettlements);
            var items = new SqlParameter("@stavke", itemsId);
            var sapId = new SqlParameter("@sap_sifra_dob", sap_id);
            var user = new SqlParameter("@korisnik", userRepository.GetLoggedUser().Result.UserName);
            _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_konacni_obracun_izmena_dobavljaca @stavke, @sap_sifra_dob, @korisnik ", items, sapId, user);
            //Optimize the code to not call the stored procedure.
            return await GetFinalSettlement(yearAndMonth.Year, yearAndMonth.Month);
        }
        public async Task<List<FinalSettlementsViewModel>> ReversalOfSettlementDialog(DateTime? date, YearAndMonth yearAndMonth, List<FinalSettlementsViewModel> finalSettlements)
        {
            string itemsId = await _refundationRepository.MakeIdFromList(finalSettlements);
            var items = new SqlParameter("@dokumenta", itemsId);
            var dateTime = new SqlParameter("@datum", date);
            var user = new SqlParameter("@korisnik", userRepository.GetLoggedUser().Result.UserName);
            _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_konacni_obracun_storniranje_i_export @dokumenta, @datum, @korisnik ", items, dateTime, user);
            //Optimize the code to not call the stored procedure.
            return await GetFinalSettlement(yearAndMonth.Year, yearAndMonth.Month);
        }
        public async Task<List<FinalSettlementsViewModel>> AcceptanceSettlement(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth)
        {
            string itemsId = await _refundationRepository.MakeIdFromList(finalSettlements);
            var items = new SqlParameter("@dokumenta", itemsId);
            var user = new SqlParameter("@korisnik", userRepository.GetLoggedUser().Result.UserName);
            _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_konacni_obracun_prihvatanje @dokumenta, @korisnik", items, user);
            //Optimize the code to not call the stored procedure.
            return await GetFinalSettlement(yearAndMonth.Year, yearAndMonth.Month);
        }
        public async Task<List<FinalSettlementsViewModel>> CancellationOfSettlement(List<FinalSettlementsViewModel> finalSettlements, YearAndMonth yearAndMonth)
        {
            string itemsId = await _refundationRepository.MakeIdFromList(finalSettlements);
            var items = new SqlParameter("@dokumenta", itemsId);
            var user = new SqlParameter("@korisnik", userRepository.GetLoggedUser().Result.UserName);
            _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_konacni_obracun_ponistavanje @dokumenta, @korisnik", items, user);
            //Optimize the code to not call the stored procedure.
            return await GetFinalSettlement(yearAndMonth.Year, yearAndMonth.Month);
        }
        public List<Partner> GetPartner(int Year, int Month)
        {
            return _context.partner.FromSqlRaw("EXEC usp_refundacije_konacni_obracun_prikaz_dobavljaca {0},{1}", Year, Month).ToList();
        }
        public int GetAlternativeSupplierFailures(int year, int month)
        {
            var godina = new SqlParameter("@godina", year);
            var mesec = new SqlParameter("@mesec", month);
            var parameterReturn = new SqlParameter
            {
                ParameterName = "ReturnValue",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
            };
            _context.Database.ExecuteSqlRaw("exec @returnValue = usp_refundacije_konacni_obracun_provera_alternativnih_dobavljaca @godina, @mesec", parameterReturn, godina, mesec);
            int returnValue = (int)parameterReturn.Value;
            return returnValue;
        }
        public void ExportFinalCalculation(int year, int month)
        {
            try
            {
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
            List<FinalSettlementHeader> headers = _context.finalSettlementHeader.Where(x => x.Active == true && x.Godina == Year && x.Mesec == Month).ToList();
            return headers[0].Status;
        }

        public void AcceptSettements(string itemsId)
        {
            //?
            //  _context.Database.ExecuteSqlRaw("EXEC usp_refundacije_PrihvacanjeTerecenjeKO_stavke {0}", itemsId);
        }
        public async Task<List<Approvals>> GetApprovals(long status)
        {
            return _context.approvals.FromSqlRaw("EXEC usp_refundacije_prikaz_odobrenja {0}", status).ToList();
        }
        public async Task<List<ApprovalStatus>> GetApprovalStatus()
        {
            return _context.approvalStatuses.ToList();
        }
        public async Task<List<Approvals>> GetNewApprovals(DateTime? documentDate, DateTime? dateOfReceipt)
        {
            return _context.approvals.FromSqlRaw("EXEC usp_refundacije_odobrenja_unos_prikaz {0},{1}", documentDate, dateOfReceipt).ToList();
        }
    }
}
