using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    internal class ConfirmedResultBuilder : IEntityTypeConfiguration<ConfirmResult>
    {
        public void Configure(EntityTypeBuilder<ConfirmResult> builder)
        {
            builder.HasMany(x => x.Comments).WithOne(x => x.ConfirmResult).HasForeignKey(x => x.ConfirmResultId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
