using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class TransferValueHistoryBuilder : IEntityTypeConfiguration<TransferValueHistory>
    {
        public void Configure(EntityTypeBuilder<TransferValueHistory> builder)
        {
            builder.HasKey(k => new { k.ObserverId, k.TargetId });


            builder.HasOne(o => o.Observer)
                .WithMany(f => f.TransfererCoins)
                .HasForeignKey(o => o.ObserverId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Target)
                .WithMany(f => f.RecieverCoins)
                .HasForeignKey(o => o.TargetId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
