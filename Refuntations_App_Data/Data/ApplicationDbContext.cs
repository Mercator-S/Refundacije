using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Refuntations_App_Data.Model;

namespace Refuntations_App.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(9000);
        }
        public DbSet<OnlineUser> users => Set<OnlineUser>();
        public DbSet<FinalSettlementHeader> finalSettlementHeader => Set<FinalSettlementHeader>();
        public DbSet<FinalSettlements> finalSettlement => Set<FinalSettlements>();
        public DbSet<Email> emails => Set<Email>();
        public DbSet<Partner> partner => Set<Partner>();
        public DbSet<EmailImport> emailsImport => Set<EmailImport>();
        public DbSet<InternalSupplier> internalSuppliers => Set<InternalSupplier>();
        public DbSet<ForeignSupplier> foreingSuppliers => Set<ForeignSupplier>();
        public DbSet<AAPdvSAPKeyMaterial> aaPdvSapKeyMaterijals => Set<AAPdvSAPKeyMaterial>();
        public DbSet<CategoryInternalOrderCostLocation> categoryInternalOrderCostLocations => Set<CategoryInternalOrderCostLocation>();
        public DbSet<CounterSapIdSapKeyAmount> counterSapIdSadKeyAmounts => Set<CounterSapIdSapKeyAmount>();
        public DbSet<Approvals> approvals => Set<Approvals>();
        public DbSet<ApprovalStatus> approvalStatuses => Set<ApprovalStatus>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }
    }

}