using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class SavedAdBuilder : IEntityTypeConfiguration<SavedAd>
    {
        public void Configure(EntityTypeBuilder<SavedAd> builder)
        {
            builder.HasKey(k => new { k.ProfileId, k.AdvertisingId });

            builder.HasOne(o => o.Profile)
                .WithMany(f => f.ProfileSavedAdvertisings)
                .HasForeignKey(o => o.ProfileId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Advertising)
                .WithMany(f => f.ProfileSavedAdvertisings)
                .HasForeignKey(o => o.AdvertisingId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
