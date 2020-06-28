using Microsoft.EntityFrameworkCore;
using PowerPointRemote.WebApi.Data.EntityConfig;
using PowerPointRemote.WebApi.Models.Entity;

namespace PowerPointRemote.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<SlideShowDetail> SlideShowDetail { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ConnectionConfiguration());
        }
    }
}