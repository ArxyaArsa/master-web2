using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class UsedActionConfig : IEntityTypeConfiguration<UsedAction>
    {
        public void Configure(EntityTypeBuilder<UsedAction> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(DataLayerConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.PartnerId).IsRequired();
            builder.Property(x => x.ActionId).IsRequired();
            builder.Property(x => x.ActionValue).HasColumnType("DECIMAL(19,4)").IsRequired();

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Partner)
                .WithMany(x => x.UsedActions)
                .HasForeignKey(x => x.PartnerId)
                .HasConstraintName("FK_UsedAction_Partner_PartnerId");
            builder.HasOne(x => x.Action)
                .WithMany(x => x.UsedActions)
                .HasForeignKey(x => x.ActionId)
                .HasConstraintName("FK_UsedAction_Action_ActionId");
            builder.HasOne(x => x.User)
                .WithMany(x => x.UsedActions)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_UsedAction_User_UserId");

            builder.ToTable("UsedAction");
        }
    }
}
