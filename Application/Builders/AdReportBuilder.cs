using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AdReportBuilder : IEntityTypeConfiguration<AdReport>
    {
        public void Configure(EntityTypeBuilder<AdReport> builder)
        {
            builder.HasKey(k => new { k.ObserverId, k.TargetId });
            builder.HasOne(o => o.Observer)
                .WithMany(f => f.AdReporters)
                .HasForeignKey(o => o.ObserverId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Target)
                .WithMany(f => f.AdReporteds)
                .HasForeignKey(o => o.TargetId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Reason)
           .WithMany(f => f.AdReports)
           .HasForeignKey(o => o.ReasonId)
           .IsRequired(true)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
