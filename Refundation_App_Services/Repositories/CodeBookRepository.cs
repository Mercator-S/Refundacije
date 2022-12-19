using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App_Data.Model;
using System.IO;


namespace Refundation_App_Services.Repositories
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
            procedureExeturor = procedureExecutor;
        }
        public async Task<IEnumerable<object>> Get(string type)
        {
            switch (type)
            {
                case "Interni dobavljači":
                    return _context.internalSuppliers.Where(e => e.active == true)
                   .ToList();
                case "Inostrani dobavljači":
                    return _context.foreingSuppliers.Where(e => e.active == true)
                .ToList();
                case "Kategorija - Interni nalog - mesto troška":
                    return _context.categoryInternalOrderCostLocations.Where(e => e.active == true)
                .ToList();
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    return _context.aaPdvSapKeyMaterijals.Where(e => e.active == true)
               .ToList();
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    return _context.counterSapIdSadKeyAmounts.Where(e => e.active == true)
               .ToList();
                case "Dobavljači - Email adrese":
                    return _context.emails.Where(e => e.active == true)
                .ToList();
            }
            return new List<object>();

        }
        public Task Add(List<object> entities, string type)
        {
            _context.AddRange(entities);
            if (type.Equals("Dobavljači - Email adrese"))
            {
                procedureExeturor.HandleNewEmailsAdded();
                _context.RemoveRange(entities);
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task<object> Delete(InternalSupplier entity)
        {
            InternalSupplier dbEntity = (InternalSupplier)_context.Find(typeof(InternalSupplier), entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }
        public async Task<object> Delete(ForeignSupplier entity)
        {
            ForeignSupplier dbEntity = (ForeignSupplier)_context.Find(typeof(ForeignSupplier), entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }
        public async Task<object> Delete(AAPdvSAPKeyMaterial entity)
        {
            AAPdvSAPKeyMaterial dbEntity = (AAPdvSAPKeyMaterial)_context.Find(typeof(AAPdvSAPKeyMaterial), entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }
        public async Task<object> Delete(CounterSapIdSapKeyAmount entity)
        {
            CounterSapIdSapKeyAmount dbEntity = (CounterSapIdSapKeyAmount)_context.Find(typeof(CounterSapIdSapKeyAmount), entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }
        public async Task<object> Delete(CategoryInternalOrderCostLocation entity)
        {
            CategoryInternalOrderCostLocation dbEntity = (CategoryInternalOrderCostLocation)_context.Find(typeof(CategoryInternalOrderCostLocation), entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }
        public async Task<object> Delete(Email entity)
        {
            Email dbEntity = _context.emails.Find(entity.id);
            dbEntity.active = false;
            dbEntity.d_upd = DateTime.Now;
            dbEntity.k_upd = loggedUser.UserName;
            _context.SaveChanges();
            return dbEntity;
        }

        public async Task<List<object>> DeleteRange(List<InternalSupplier> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (InternalSupplier entity in entities)
            {
                InternalSupplier dbEntity = (InternalSupplier)_context.Find(typeof(InternalSupplier), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }

        public async Task<List<object>> DeleteRange(List<ForeignSupplier> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (ForeignSupplier entity in entities)
            {
                ForeignSupplier dbEntity = (ForeignSupplier)_context.Find(typeof(ForeignSupplier), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }

        public async Task<List<object>> DeleteRange(List<AAPdvSAPKeyMaterial> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (AAPdvSAPKeyMaterial entity in entities)
            {
                AAPdvSAPKeyMaterial dbEntity = (AAPdvSAPKeyMaterial)_context.Find(typeof(AAPdvSAPKeyMaterial), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }

        public async Task<List<object>> DeleteRange(List<CounterSapIdSapKeyAmount> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (CounterSapIdSapKeyAmount entity in entities)
            {
                CounterSapIdSapKeyAmount dbEntity = (CounterSapIdSapKeyAmount)_context.Find(typeof(CounterSapIdSapKeyAmount), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }

        public async Task<List<object>> DeleteRange(List<CategoryInternalOrderCostLocation> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (CategoryInternalOrderCostLocation entity in entities)
            {
                CategoryInternalOrderCostLocation dbEntity = (CategoryInternalOrderCostLocation)_context.Find(typeof(CategoryInternalOrderCostLocation), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }

        public async Task<List<object>> DeleteRange(List<Email> entities, string targetGroup)
        {
            List<object> dbEntities = new List<object>();
            foreach (Email entity in entities)
            {
                Email dbEntity = (Email)_context.Find(typeof(Email), entity.id);
                dbEntity.active = false;
                dbEntity.d_upd = DateTime.Now;
                dbEntity.k_upd = loggedUser.UserName;
                dbEntities.Add(dbEntity);
            }
            _context.SaveChanges();
            return dbEntities;
        }
    }

}
