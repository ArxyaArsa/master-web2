using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserConfig : IEntityTypeConfiguration<DiscountsUser>
    {
        public void Configure(EntityTypeBuilder<DiscountsUser> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.AccessFailedCount);
            builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.EmailConfirmed);
            builder.Property(x => x.LockoutEnabled);
            builder.Property(x => x.LockoutEnd);
            builder.Property(x => x.NormalizedEmail).HasMaxLength(256);
            builder.Property(x => x.NormalizedUserName).HasMaxLength(256);
            builder.Property(x => x.PasswordHash);
            builder.Property(x => x.PhoneNumber);
            builder.Property(x => x.PhoneNumberConfirmed);
            builder.Property(x => x.SecurityStamp);
            builder.Property(x => x.TwoFactorEnabled);
            builder.Property(x => x.UserName).HasMaxLength(256);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedEmail).HasName("EmailIndex");
            builder.HasIndex(x => x.NormalizedUserName)
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

            builder.ToTable("AspNetUsers");
        }
    }
}
