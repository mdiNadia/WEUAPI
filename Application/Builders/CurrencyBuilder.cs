using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class CurrencyBuilder : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasMany(c => c.Countries).WithOne(c => c.Currency).HasForeignKey(c => c.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
