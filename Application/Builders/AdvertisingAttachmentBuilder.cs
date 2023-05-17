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
    public class AdvertisingAttachmentBuilder : IEntityTypeConfiguration<AdvertisingAttachment>
    {
        public void Configure(EntityTypeBuilder<AdvertisingAttachment> builder)
        {
            builder.HasKey(k => new { k.AttachmentId, k.AdvertisingId });
           

        }
    }
}
