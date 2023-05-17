using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AdvertisingBuilder : IEntityTypeConfiguration<Advertising>
    {
        public void Configure(EntityTypeBuilder<Advertising> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate).IsRequired(false);
            builder.Property(x => x.ExpireDate).IsRequired(false);
            builder.Property(x => x.BoostId).IsRequired(false);
            //Relations
            builder.HasOne(x => x.Boost).WithOne(x => x.Advertising).HasForeignKey<Advertising>(x => x.BoostId);

        }
    }
}
