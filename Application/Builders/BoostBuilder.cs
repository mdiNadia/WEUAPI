using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class BoostBuilder : IEntityTypeConfiguration<Boost>
    {
        public void Configure(EntityTypeBuilder<Boost> builder)
        {
            builder.HasOne(x => x.Advertising).WithOne(x => x.Boost).HasForeignKey<Boost>(x => x.AdvertisingId);
        }
    }
}
