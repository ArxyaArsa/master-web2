using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(DataLayerConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.DiscountsUserId);
            builder.Property(x => x.PartnerId);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.PathToFile).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.FilterJson).HasMaxLength(5000).IsRequired();

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Partner)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.PartnerId)
                .HasConstraintName("FK_Report_Partner_PartnerId");
            builder.HasOne(x => x.DiscountsUser)
                .WithMany(x => x.ReportsOfMyCreation)
                .HasForeignKey(x => x.DiscountsUserId)
                .HasConstraintName("FK_Report_AspNetUsers_DiscountsUserId");

            builder.ToTable("Report");
        }
    }
}
