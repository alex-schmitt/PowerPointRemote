﻿using Microsoft.EntityFrameworkCore;
using PowerPointRemote.WebApi.Data.EntityConfig;
using PowerPointRemote.WebApi.Models.EntityFramework;

namespace PowerPointRemote.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChannelConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}