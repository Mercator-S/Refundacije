﻿using Microsoft.AspNetCore.Identity;
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
        }
        public DbSet<OnlineUser> users => Set<OnlineUser>();
        public DbSet<FinalSettlements> finalSettlement => Set<FinalSettlements>();
        public DbSet<Email> emails => Set<Email>();
        public DbSet<EmailImport> emailsImport => Set<EmailImport>();
        public DbSet<InternalSupplier> internalSuppliers => Set<InternalSupplier>();
        public DbSet<ForeignSupplier> foreingSuppliers => Set<ForeignSupplier>();
        public DbSet<AAPdvSAPKeyMaterial> aaPdvSapKeyMaterijals => Set<AAPdvSAPKeyMaterial>();
        public DbSet<CategoryInternalOrderCostLocation> categoryInternalOrderCostLocations => Set<CategoryInternalOrderCostLocation>();
        public DbSet<CounterSapIdSapKeyAmount> counterSapIdSadKeyAmounts => Set<CounterSapIdSapKeyAmount>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
        }
    }
}