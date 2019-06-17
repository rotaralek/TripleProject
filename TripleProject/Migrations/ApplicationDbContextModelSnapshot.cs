﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripleProject.Data;

namespace TripleProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Currency");

                    b.Property<DateTime?>("DateTime");

                    b.Property<string>("GalleryId");

                    b.Property<int?>("GalleryId1");

                    b.Property<int?>("ImageId");

                    b.Property<decimal?>("Price");

                    b.Property<int?>("Quantity");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserId");

                    b.Property<int?>("Views");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId1");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.AdvertisementAttribute", b =>
                {
                    b.Property<int>("AdvertisementId");

                    b.Property<int>("AttributeId");

                    b.HasKey("AdvertisementId", "AttributeId");

                    b.HasIndex("AttributeId");

                    b.ToTable("AdvertisementsAttributes");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.AdvertisementCategory", b =>
                {
                    b.Property<int>("AdvertisementId");

                    b.Property<int>("CategoryId");

                    b.HasKey("AdvertisementId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AdvertisementsCategories");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("Order");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Target");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Currency");

                    b.Property<DateTime?>("DateTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("GalleryId");

                    b.Property<int?>("GalleryId1");

                    b.Property<int?>("ImageId");

                    b.Property<decimal?>("Price");

                    b.Property<int?>("Quantity");

                    b.Property<int?>("ShopId");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserId");

                    b.Property<int?>("Views");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId1");

                    b.HasIndex("ImageId");

                    b.HasIndex("ShopId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.ProductAttribute", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("AttributeId");

                    b.HasKey("ProductId", "AttributeId");

                    b.HasIndex("AttributeId");

                    b.ToTable("ProductsAttributes");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.ProductCatalog", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("CatalogId");

                    b.HasKey("ProductId", "CatalogId");

                    b.HasIndex("CatalogId");

                    b.ToTable("ProductsCatalogs");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Email")
                        .HasMaxLength(200);

                    b.Property<string>("GalleryId");

                    b.Property<int?>("GalleryId1");

                    b.Property<int?>("ImageId");

                    b.Property<string>("Location")
                        .HasMaxLength(200);

                    b.Property<string>("Phone")
                        .HasMaxLength(100);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserId");

                    b.Property<int?>("Views");

                    b.Property<string>("WorkingHours")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("GalleryId1");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Advertisement", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Gallery")
                        .WithMany()
                        .HasForeignKey("GalleryId1");

                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.AdvertisementAttribute", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Advertisement", "Advertisement")
                        .WithMany("AdvertisementsAttributes")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TripleProject.Areas.Admin.Models.Attribute", "Attribute")
                        .WithMany("AdvertisementsAttributes")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.AdvertisementCategory", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Advertisement", "Advertisement")
                        .WithMany("AdvertisemetsCategories")
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TripleProject.Areas.Admin.Models.Category", "Category")
                        .WithMany("AdvertisemetsCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Attribute", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Attribute", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Catalog", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Catalog", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Category", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Menu", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Product", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Gallery")
                        .WithMany()
                        .HasForeignKey("GalleryId1");

                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("TripleProject.Areas.Admin.Models.Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.ProductAttribute", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Attribute", "Attribute")
                        .WithMany("ProductsAttributes")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TripleProject.Areas.Admin.Models.Product", "Product")
                        .WithMany("ProductsAttributes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.ProductCatalog", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.Catalog", "Catalog")
                        .WithMany("ProductsCatalogs")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TripleProject.Areas.Admin.Models.Product", "Product")
                        .WithMany("ProductsCatalogs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripleProject.Areas.Admin.Models.Shop", b =>
                {
                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Gallery")
                        .WithMany()
                        .HasForeignKey("GalleryId1");

                    b.HasOne("TripleProject.Areas.Admin.Models.FileUpload", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
