using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<DiscountsRole>
    {
        public void Configure(EntityTypeBuilder<DiscountsRole> builder)
        {
            builder.Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken();
            builder.Property(x => x.Name)
                .HasMaxLength(256);
            builder.Property(x => x.NormalizedName)
                .HasMaxLength(256);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName)
                .IsUnique()
                .HasName("RoleNameIndex")
                .HasFilter("[NormalizedName] IS NOT NULL");

            builder.ToTable("AspNetRoles");
        }
    }
}
