using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class PartnerConfig : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.PartnerTypeId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PartnerType)
                .WithMany(x => x.Partners)
                .HasForeignKey(x => x.PartnerTypeId)
                .HasConstraintName("FK_Partner_PartnerType_PartnerTypeId");

            builder.ToTable("Partner");
        }
    }
}
