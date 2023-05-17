using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AdCategoryCostBuilder : IEntityTypeConfiguration<AdCategoryCost>
    {
        public void Configure(EntityTypeBuilder<AdCategoryCost> builder)
        {
            builder.HasOne(x => x.AdCategory).WithOne(x => x.CategoryCost).HasForeignKey<AdCategory>(x => x.CategoryCostId);


        }
    }
}
