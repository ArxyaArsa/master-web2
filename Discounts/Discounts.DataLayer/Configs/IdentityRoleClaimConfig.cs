using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;

namespace Discounts.DataLayer.Configs
{
    public class IdentityRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.Property(x => x.Id)
                        .ValueGeneratedOnAdd()
                        .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.ClaimType);
            builder.Property(x => x.ClaimValue);
            builder.Property(x => x.RoleId)
                .IsRequired();
            
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.RoleId);

            builder.ToTable("AspNetRoleClaims");

            builder.HasOne(typeof(IdentityRole<int>))
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
