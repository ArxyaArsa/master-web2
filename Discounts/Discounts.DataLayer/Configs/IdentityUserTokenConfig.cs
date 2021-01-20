using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<int>> builder)
        {
            builder.Property(x => x.UserId);
            builder.Property(x => x.LoginProvider)
                .HasMaxLength(128);
            builder.Property(x => x.Name)
                .HasMaxLength(128);
            builder.Property(x => x.Value);

            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            builder.ToTable("AspNetUserTokens");

            builder.HasOne(typeof(IdentityUser<int>))
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
