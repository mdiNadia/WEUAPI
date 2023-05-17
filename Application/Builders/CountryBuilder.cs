using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class CountryBuilder : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Iso).HasMaxLength(2);
            builder.Property(c => c.Iso3).HasMaxLength(3).IsRequired(false);
            builder.Property(c => c.Name).HasMaxLength(80);
            builder.Property(c => c.PhoneCode).IsRequired(false);
            builder.Property(c => c.NumCode).IsRequired(false);

            builder.HasMany(x => x.Provinces).WithOne(x => x.Country).HasForeignKey(x => x.CountryId);
        }
    }
}
