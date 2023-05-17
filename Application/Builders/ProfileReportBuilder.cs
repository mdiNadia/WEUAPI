using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileReportBuilder : IEntityTypeConfiguration<ProfileReport>
    {
        public void Configure(EntityTypeBuilder<ProfileReport> builder)
        {

            builder.HasOne(o => o.Observer)
                .WithMany(f => f.Reporters)
                .HasForeignKey(o => o.ObserverId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Target)
                .WithMany(f => f.Reporteds)
                .HasForeignKey(o => o.TargetId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Reason)
            .WithMany(f => f.ProfileReports)
            .HasForeignKey(o => o.ReasonId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
