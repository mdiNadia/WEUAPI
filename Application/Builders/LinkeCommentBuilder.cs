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
    public class LinkeCommentBuilder : IEntityTypeConfiguration<LikeComment>
    {
        public void Configure(EntityTypeBuilder<LikeComment> builder)
        {
            builder.HasKey(k => new { k.ObserverId, k.TargetId });

           
        }
    }
}
