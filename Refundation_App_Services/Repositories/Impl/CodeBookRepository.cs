using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;


namespace Refundation_App_Services.Repositories.Impl
{
    public class CodeBookRepository : ICodeBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository userRepository;
        private OnlineUser loggedUser;
        private readonly IProcedureExecutor procedureExeturor;
        public CodeBookRepository(ApplicationDbContext context, IUserRepository userRepository, IProcedureExecutor procedureExecutor)
        {
            _context = context;
            this.userRepository = userRepository;
            loggedUser = userRepository.GetLoggedUser().Result;
            this.procedureExeturor= procedureExecutor;
        }

        public Task AddAAPdvSAPKeyMaterials(List<AAPdvSAPKeyMaterial> entities)
        {
            _context.aaPdvSapKeyMaterijals.AddRange(entities);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddCategoryInternalOrderCostLocation(List<CategoryInternalOrderCostLocation> entities)
        {
            _context.categoryInternalOrderCostLocations.AddRange(entities);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddCounterSAPIdAmount(List<CounterSapIdSapKeyAmount> entities)
        {
            _context.counterSapIdSadKeyAmounts.AddRange(entities);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddEmails(List<EmailImport> mails)
        {
            _context.emailsImport.AddRange(mails);
            procedureExeturor.HandleNewEmailsAdded();
            _context.emailsImport.RemoveRange(mails);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddForeignSuppliers(List<ForeignSupplier> suppliersList)
        {
            _context.foreingSuppliers.AddRange(suppliersList);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddInternalSuppliers(List<InternalSupplier> suppliersList)
        {
            _context.internalSuppliers.AddRange(suppliersList);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<AAPdvSAPKeyMaterial> DeleteAAPdvSAPKeyMaterial(int id)
        {
            AAPdvSAPKeyMaterial dbEntity = _context.aaPdvSapKeyMaterijals.Find(id);
            dbEntity.active = false;
            dbEntity.d_upd= DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }

        public async Task<CategoryInternalOrderCostLocation> DeleteCategoryInternalOrderCostLocation(int id)
        {
            CategoryInternalOrderCostLocation dbEntity = _context.categoryInternalOrderCostLocations.Find(id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }

        public async Task<CounterSapIdSapKeyAmount> DeleteCounterSAPIdAmount(int id)
        {
            CounterSapIdSapKeyAmount dbEntity= _context.counterSapIdSadKeyAmounts.Find(id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;

        }

        public async Task<ForeignSupplier> DeleteForeignSupplier(int id)
        {
            ForeignSupplier dbentity = _context.foreingSuppliers.Find(id);
            dbentity.active = false;
            dbentity.d_upd = DateTime.Now;
            dbentity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbentity;
        }

        public async Task<InternalSupplier> DeleteInternalSupplier(int id)
        {
            InternalSupplier dbentity= _context.internalSuppliers.Find(id);
            dbentity.active = false;
            dbentity.d_upd = DateTime.Now;
            dbentity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbentity;
        }

        public async Task<Email> DeleteSuppliersEmail(int id)
        {
            Email dbentity = _context.emails.Find(id);
            dbentity.active = false;
            dbentity.d_upd = DateTime.Now;
            dbentity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbentity;
        }

        public async Task<IEnumerable<AAPdvSAPKeyMaterial>> GetActivitiesWithSAPKeyAndMaterial()
        {
            return _context.aaPdvSapKeyMaterijals.Where(e => e.active == true)
                .ToList();
        }

        public async Task<IEnumerable<CategoryInternalOrderCostLocation>> GetCategoriesWithInternalOrderAndCostLocation()
        {
            return _context.categoryInternalOrderCostLocations.Where(e => e.active == true)
                .ToList();
        }

        public async Task<IEnumerable<CounterSapIdSapKeyAmount>> GetCountersWithSAPIdAndAmount()
        {
            return _context.counterSapIdSadKeyAmounts.Where(e => e.active == true)
                .ToList();
        }

        public async Task<IEnumerable<Email>> GetEmails()
        {
            return _context.emails.Where(e => e.active == true)
                .ToList();
        }

        public async Task<IEnumerable<ForeignSupplier>> GetForeignSuppliers()
        {
            return _context.foreingSuppliers.Where(e => e.active == true)
                .ToList();
        }

        public async Task<IEnumerable<InternalSupplier>> GetInternalSuppliers()
        {
            return  _context.internalSuppliers.Where(e => e.active == true)
                .ToList();
        }
    }
}
