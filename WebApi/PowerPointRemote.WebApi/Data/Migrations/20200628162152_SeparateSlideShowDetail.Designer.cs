﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PowerPointRemote.WebAPI.Data;

namespace PowerPointRemote.WebApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200628162152_SeparateSlideShowDetail")]
    partial class SeparateSlideShowDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.Channel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("char(9)");

                    b.Property<bool>("ChannelEnded")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("HostConnectionId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.SlideShowDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ChannelId")
                        .HasColumnType("char(9)");

                    b.Property<int>("CurrentSlide")
                        .HasColumnType("int");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TotalSlides")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId")
                        .IsUnique();

                    b.ToTable("SlideShowDetail");
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ChannelId")
                        .IsRequired()
                        .HasColumnType("char(9)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.UserConnection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ChannelId")
                        .IsRequired()
                        .HasColumnType("char(9)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConnections");
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.SlideShowDetail", b =>
                {
                    b.HasOne("PowerPointRemote.WebApi.Models.Entity.Channel", "Channel")
                        .WithOne("SlideShowDetail")
                        .HasForeignKey("PowerPointRemote.WebApi.Models.Entity.SlideShowDetail", "ChannelId");
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.User", b =>
                {
                    b.HasOne("PowerPointRemote.WebApi.Models.Entity.Channel", "Channel")
                        .WithMany("Users")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PowerPointRemote.WebApi.Models.Entity.UserConnection", b =>
                {
                    b.HasOne("PowerPointRemote.WebApi.Models.Entity.Channel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PowerPointRemote.WebApi.Models.Entity.User", "User")
                        .WithMany("Connections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
