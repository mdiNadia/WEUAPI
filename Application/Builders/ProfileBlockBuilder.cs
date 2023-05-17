using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileBlockBuilder : IEntityTypeConfiguration<ProfileBlock>
    {
        public void Configure(EntityTypeBuilder<ProfileBlock> builder)
        {
            builder.HasKey(k => new { k.ObserverId, k.TargetId });

            builder.HasOne(o => o.Observer)
                .WithMany(f => f.BlockerUsers)
                .HasForeignKey(o => o.ObserverId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Target)
                .WithMany(f => f.BlockedUsers)
                .HasForeignKey(o => o.TargetId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ProfileReport)
                 .WithMany(f => f.ProfileBlocks)
                 .HasForeignKey(o => o.ProfileReportId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
