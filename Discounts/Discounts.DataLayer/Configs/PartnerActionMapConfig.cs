using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class PartnerActionMapConfig : IEntityTypeConfiguration<PartnerActionMap>
    {
        public void Configure(EntityTypeBuilder<PartnerActionMap> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.PartnerId).IsRequired();
            builder.Property(x => x.ActionId).IsRequired();
            builder.Property(x => x.CreatedDate);

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Partner)
                .WithMany(x => x.PartnerActionMaps)
                .HasForeignKey(x => x.PartnerId)
                .HasConstraintName("FK_PartnerActionMap_Partner_PartnerId");
            builder.HasOne(x => x.Action)
                .WithMany(x => x.PartnerActionMaps)
                .HasForeignKey(x => x.ActionId)
                .HasConstraintName("FK_PartnerActionMap_Action_ActionId");

            builder.ToTable("PartnerActionMap");
        }
    }
}
