using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class CountryBuilder : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasMany(x => x.Provinces).WithOne(x => x.Country).HasForeignKey(x => x.CountryId);
        }
    }
}
