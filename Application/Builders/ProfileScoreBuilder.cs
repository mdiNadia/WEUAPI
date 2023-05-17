using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileScoreBuilder : IEntityTypeConfiguration<ProfileScore>
    {
        public void Configure(EntityTypeBuilder<ProfileScore> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IconId).IsRequired(false);

        }
    }
}
