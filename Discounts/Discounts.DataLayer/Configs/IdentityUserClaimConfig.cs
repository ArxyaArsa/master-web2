using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
        {
            builder.Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.ClaimType);
            builder.Property(x => x.ClaimValue);
            builder.Property(x => x.UserId)
                .IsRequired();
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId);

            builder.ToTable("AspNetUserClaims");

            builder.HasOne(typeof(DiscountsUser))
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
