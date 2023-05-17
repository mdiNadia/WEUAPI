using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class GroupBuilder : IEntityTypeConfiguration<Group>
    {


        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                    .HasMany(x => x.Connections)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
