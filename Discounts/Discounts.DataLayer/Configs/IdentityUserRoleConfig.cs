using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class IdentityUserRoleConfig : IEntityTypeConfiguration<DiscountsUserRole>
    {
        public void Configure(EntityTypeBuilder<DiscountsUserRole> builder)
        {
            builder.Property(x => x.UserId);
            builder.Property(x => x.RoleId);
            
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.HasIndex(x => x.RoleId);
            
            builder.ToTable("AspNetUserRoles");

            builder.HasOne(x => x.Role)
                        .WithMany(x => x.UserRoleMaps)
                        .HasForeignKey(x => x.RoleId)
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoleMaps)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
