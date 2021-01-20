using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.Property(x => x.UserId);
            builder.Property(x => x.RoleId);
            
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.HasIndex(x => x.RoleId);
            
            builder.ToTable("AspNetUserRoles");

            builder.HasOne(typeof(IdentityRole<int>))
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(typeof(IdentityUser<int>))
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
