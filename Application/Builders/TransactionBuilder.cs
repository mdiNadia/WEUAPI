using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class TransactionBuilder : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne<TransactionType>(x => x.TransactionType).WithOne(x => x.Transaction).HasForeignKey<TransactionType>(x => x.TransactionId);
            builder.HasOne<TransactionStatus>(x => x.TransactionStatus).WithOne(x => x.Transaction).HasForeignKey<TransactionStatus>(x => x.TransactionId);
            builder.HasOne(x=>x.Order).WithMany(x=>x.Transactions).HasForeignKey(x=>x.OrderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
