using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPointRemote.WebApi.Models.Entity;

namespace PowerPointRemote.WebApi.Data.EntityConfig
{
    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.Property(c => c.Id).HasColumnType("char(9)");
        }
    }
}