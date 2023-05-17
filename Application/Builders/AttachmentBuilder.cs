using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AttachmentBuilder : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasMany(x => x.Profiles).WithOne(x => x.Avatar).HasForeignKey(x => x.AvatarId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ProfileScores).WithOne(x => x.Icon).HasForeignKey(x => x.IconId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.FileTypeId).IsRequired(false);
        }
    }
}
