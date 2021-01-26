using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.Property(x => x.LoginProvider)
                        .HasMaxLength(128);
            builder.Property(x => x.ProviderKey)
                .HasMaxLength(128);
            builder.Property(x => x.ProviderDisplayName);
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            builder.HasIndex(x => x.UserId);

            builder.ToTable("AspNetUserLogins");

            builder.HasOne(typeof(DiscountsUser))
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
