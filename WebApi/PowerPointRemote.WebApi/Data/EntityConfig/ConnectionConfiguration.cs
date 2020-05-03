using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPointRemote.WebApi.Models.Entity;

namespace PowerPointRemote.WebApi.Data.EntityConfig
{
    public class ConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.Property(conn => conn.ChannelId).IsRequired();
        }
    }
}