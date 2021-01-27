using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Discounts.DataLayer.Helpers;
using Discounts.DataLayer.Models;

namespace Discounts.DataLayer.Configs
{
    public class PartnerTypeConfig : IEntityTypeConfiguration<PartnerType>
    {
        public void Configure(EntityTypeBuilder<PartnerType> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(Constants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.Name).HasMaxLength(250);

            builder.HasKey(x => x.Id);

            builder.ToTable("PartnerType");
        }
    }
}
