using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Application.Builders
{
    public class ReportReasonBuilder : IEntityTypeConfiguration<ReportReason>
    {
        public void Configure(EntityTypeBuilder<ReportReason> builder)
        {
            builder.HasMany(c => c.Children).WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
