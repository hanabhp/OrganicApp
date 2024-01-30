﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using waterfood.Data.Context;

#nullable disable

namespace waterfood.Data.Migrations
{
    [DbContext(typeof(WaterFoodContext))]
    [Migration("20230416174815_edit_items2")]
    partial class edit_items2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("waterfood.Data.Entities.Centers.Center", b =>
                {
                    b.Property<int>("CenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CenterId"), 1L, 1);

                    b.Property<string>("CenterImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Creator")
                        .HasColumnType("int");

                    b.Property<int?>("LocationRef")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerRef")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ScheduledTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("CenterId");

                    b.HasIndex("Creator");

                    b.HasIndex("LocationRef");

                    b.HasIndex("OwnerRef");

                    b.ToTable("Centers");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<int?>("Parent")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.HasIndex("Parent");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"), 1L, 1);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<int>("MenuRef")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuItemId");

                    b.HasIndex("MenuRef");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Items.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<int>("CategoryRef")
                        .HasColumnType("int");

                    b.Property<int>("CenterRef")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Left")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryRef");

                    b.HasIndex("CenterRef");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Users.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Users.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("ActiveCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Creator")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocationRef")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QrToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleRef")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<string>("UserAvatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("LocationRef");

                    b.HasIndex("RoleRef");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Centers.Center", b =>
                {
                    b.HasOne("waterfood.Data.Entities.Users.User", "CreatedBy")
                        .WithMany("CreatedByUsers")
                        .HasForeignKey("Creator");

                    b.HasOne("waterfood.Data.Entities.Generals.Location", "Location")
                        .WithMany("Centers")
                        .HasForeignKey("LocationRef");

                    b.HasOne("waterfood.Data.Entities.Users.User", "OwnerUser")
                        .WithMany("OwnerUsers")
                        .HasForeignKey("OwnerRef")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Location");

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Category", b =>
                {
                    b.HasOne("waterfood.Data.Entities.Generals.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("Parent");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.MenuItem", b =>
                {
                    b.HasOne("waterfood.Data.Entities.Generals.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuRef")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Items.Item", b =>
                {
                    b.HasOne("waterfood.Data.Entities.Generals.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryRef")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("waterfood.Data.Entities.Centers.Center", "Center")
                        .WithMany("Items")
                        .HasForeignKey("CenterRef")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Center");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Users.User", b =>
                {
                    b.HasOne("waterfood.Data.Entities.Generals.Location", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationRef");

                    b.HasOne("waterfood.Data.Entities.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleRef")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Centers.Center", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Location", b =>
                {
                    b.Navigation("Centers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Generals.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Users.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("waterfood.Data.Entities.Users.User", b =>
                {
                    b.Navigation("CreatedByUsers");

                    b.Navigation("OwnerUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
