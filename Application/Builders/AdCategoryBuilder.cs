using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class AdCategoryBuilder : IEntityTypeConfiguration<AdCategory>
    {
        public void Configure(EntityTypeBuilder<AdCategory> builder)
        {
            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.CategoryCost).WithOne(x => x.AdCategory).HasForeignKey<AdCategoryCost>(x => x.AdCategoryId);

        }
    }
}
