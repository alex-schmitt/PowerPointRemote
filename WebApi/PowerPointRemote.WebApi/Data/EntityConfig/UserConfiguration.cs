using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPointRemote.WebApi.Models.EntityFramework;

namespace PowerPointRemote.WebApi.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(255);
            builder.Property(u => u.ChannelId).IsRequired();
        }
    }
}