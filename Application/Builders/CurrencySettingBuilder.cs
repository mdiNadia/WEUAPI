using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class CurrencySettingBuilder : IEntityTypeConfiguration<CurrencySetting>
    {
        public void Configure(EntityTypeBuilder<CurrencySetting> builder)
        {
            builder.HasOne(c => c.Currency).WithMany(c => c.CurrencySettings).HasForeignKey(c => c.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
