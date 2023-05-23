using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileBuilder : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {

            //Relations
            builder.HasMany(x => x.Advertisings).WithOne(x => x.Advertiser).HasForeignKey(x => x.AdvertiserId);
            builder.HasMany(x => x.ProfileSavedAdvertisings).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Avatar).WithMany(x => x.Profiles).HasForeignKey(x => x.AvatarId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Profiles).HasForeignKey(x => x.UserId).IsRequired(true);
            builder.HasOne(x => x.Wallet).WithOne(x => x.Profile).HasForeignKey<Wallet>(x => x.ProfileId);


        }
    }
}
