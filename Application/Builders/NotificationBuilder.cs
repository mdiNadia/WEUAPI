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
    public class NotificationBuilder : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {

            builder.HasKey(k => new { k.ObserverId , k.Id });
            builder.HasOne(o => o.Target)
              .WithMany(f => f.Notified)
              .HasForeignKey(o => o.TargetId)
              .IsRequired(true)
              .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Observer)
                .WithMany(f => f.Notifier)
                .HasForeignKey(o => o.ObserverId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
           

        }
    }
}
