using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ConfirmedResultAttachmentBuilder : IEntityTypeConfiguration<ConfirmedResultAttachment>
    {
        public void Configure(EntityTypeBuilder<ConfirmedResultAttachment> builder)
        {
            builder.HasKey(k => new { k.ConfirmResultId, k.AttachmentId });

        }
    }
}
