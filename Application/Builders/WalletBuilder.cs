using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class WalletBuilder : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasMany(x => x.Transactions).WithOne(x => x.Wallet).HasForeignKey(x => x.WalletId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Currency).WithMany(x => x.Wallets).HasForeignKey(x => x.CurrencyId);
        }
    }
}
