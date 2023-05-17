using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class FileTypeBuilder : IEntityTypeConfiguration<FileType>
    {
        public void Configure(EntityTypeBuilder<FileType> builder)
        {
            builder.HasMany(x => x.Attachments).WithOne(x => x.FileType).HasForeignKey(x => x.FileTypeId);
        }
    }
}
