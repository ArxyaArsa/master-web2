using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class DiscountActionConfig : IEntityTypeConfiguration<DiscountAction>
    {
        public void Configure(EntityTypeBuilder<DiscountAction> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.CashValue).HasColumnType("DECIMAL(19,2)");
            builder.Property(x => x.PercentValue).HasColumnType("DECIMAL(19,4)");
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);
            builder.Property(x => x.IsCanceled);
            builder.Property(x => x.CancelDate);
            builder.Property(x => x.CancelReason).HasMaxLength(1000);

            builder.HasKey(x => x.Id);

            builder.ToTable("Action");
        }
    }
}
