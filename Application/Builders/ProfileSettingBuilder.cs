using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Builders
{
    public class ProfileSettingBuilder : IEntityTypeConfiguration<ProfileSetting>
    {
        public void Configure(EntityTypeBuilder<ProfileSetting> builder)
        {
        }
    }
}
