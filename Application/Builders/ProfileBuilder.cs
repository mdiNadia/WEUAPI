using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileBuilder : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Username).IsRequired(true);
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.Bio).IsRequired(false);
            builder.Property(x => x.Link).IsRequired(false);
            builder.Property(x => x.ProfileTypeEnum).IsRequired(true);
            builder.Property(x => x.AvatarId).IsRequired(false);
            //Relations
            builder.HasMany(x => x.Advertisings).WithOne(x => x.Advertiser).HasForeignKey(x => x.AdvertiserId);
            builder.HasMany(x => x.ProfileSavedAdvertisings).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Avatar).WithMany(x => x.Profiles).HasForeignKey(x => x.AvatarId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Profiles).HasForeignKey(x => x.UserId).IsRequired(true);
            // builder.HasMany(x=>x.MessagesSent).WithOne(x=>x.Sender).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            //builder.HasMany(x => x.MessagesReceived).WithOne(x => x.Recipient).HasForeignKey(x=>x.RecipientId).OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            builder.HasOne(x => x.Wallet).WithOne(x => x.Profile).HasForeignKey<Wallet>(x => x.ProfileId);


        }
    }
}
