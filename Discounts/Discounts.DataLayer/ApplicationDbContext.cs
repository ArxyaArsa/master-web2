using Discounts.DataLayer.Configs;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<DiscountsUser, IdentityRole<int>, int>
    {
        public DbSet<DiscountsUser> DiscountUser { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<DiscountAction> DiscountAction { get; set; }
        public DbSet<PartnerType> PartnerType { get; set; }
        public DbSet<PartnerActionMap> PartnerActionMap { get; set; }
        public DbSet<UsedAction> UsedAction { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0")
                .HasAnnotation(Constants.Relational_MaxIdentifierLength, 128)
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.ApplyConfiguration(new IdentityUserConfig());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfig());
            modelBuilder.ApplyConfiguration(new IdentityRoleClaimConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserClaimConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());

            modelBuilder.ApplyConfiguration(new DiscountActionConfig());
            modelBuilder.ApplyConfiguration(new PartnerActionMapConfig());
            modelBuilder.ApplyConfiguration(new PartnerTypeConfig());
            modelBuilder.ApplyConfiguration(new PartnerConfig());
            modelBuilder.ApplyConfiguration(new UsedActionConfig());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
