using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AttachmentBuilder : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {

            builder.HasMany(x => x.ProfileScores).WithOne(x => x.Icon).HasForeignKey(x => x.IconId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
