using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class RejectedResultAttachmentBuilder : IEntityTypeConfiguration<RejectedResultAttachment>
    {
        public void Configure(EntityTypeBuilder<RejectedResultAttachment> builder)
        {
            builder.HasKey(k => new { k.RejectResultId, k.AttachmentId });


        }
    }
}
