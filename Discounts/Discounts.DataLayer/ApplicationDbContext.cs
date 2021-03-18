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
    public class ApplicationDbContext : IdentityDbContext<DiscountsUser, DiscountsRole, int, IdentityUserClaim<int>, DiscountsUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DbSet<Partner> Partner { get; set; }
        public DbSet<DiscountAction> DiscountAction { get; set; }
        public DbSet<PartnerType> PartnerType { get; set; }
        public DbSet<PartnerActionMap> PartnerActionMap { get; set; }
        public DbSet<UsedAction> UsedAction { get; set; }
        public DbSet<Report> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0")
                .HasAnnotation(DataLayerConstants.Relational_MaxIdentifierLength, 128)
                .HasAnnotation(DataLayerConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

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
            modelBuilder.ApplyConfiguration(new ReportConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
