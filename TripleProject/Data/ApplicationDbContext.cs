using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Areas.Admin.ViewModels;

namespace TripleProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Areas.Admin.Models.Attribute> Attributes { get; set; }
        public DbSet<ProductCatalog> ProductsCatalogs { get; set; }
        public DbSet<AdvertisementCategory> AdvertisementsCategories { get; set; }
        public DbSet<ProductAttribute> ProductsAttributes { get; set; }
        public DbSet<AdvertisementAttribute> AdvertisementsAttributes { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ProductCatalog
            modelBuilder.Entity<ProductCatalog>()
            .HasKey(t => new { t.ProductId, t.CatalogId });

            modelBuilder.Entity<ProductCatalog>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductsCatalogs)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductCatalog>()
                .HasOne(pt => pt.Catalog)
                .WithMany(t => t.ProductsCatalogs)
                .HasForeignKey(pt => pt.CatalogId);

            //AdvertisementCategory
            modelBuilder.Entity<AdvertisementCategory>()
            .HasKey(t => new { t.AdvertisementId, t.CategoryId });

            modelBuilder.Entity<AdvertisementCategory>()
                .HasOne(pt => pt.Advertisement)
                .WithMany(p => p.AdvertisemetsCategories)
                .HasForeignKey(pt => pt.AdvertisementId);

            modelBuilder.Entity<AdvertisementCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(t => t.AdvertisemetsCategories)
                .HasForeignKey(pt => pt.CategoryId);

            //ProductAttribute
            modelBuilder.Entity<ProductAttribute>()
            .HasKey(t => new { t.ProductId, t.AttributeId });

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductsAttributes)
                .HasForeignKey(pt => pt.ProductId);

            modelBuilder.Entity<ProductAttribute>()
                .HasOne(pt => pt.Attribute)
                .WithMany(t => t.ProductsAttributes)
                .HasForeignKey(pt => pt.AttributeId);

            //AdvertisementAttribute
            modelBuilder.Entity<AdvertisementAttribute>()
            .HasKey(t => new { t.AdvertisementId, t.AttributeId });

            modelBuilder.Entity<AdvertisementAttribute>()
                .HasOne(pt => pt.Advertisement)
                .WithMany(p => p.AdvertisementsAttributes)
                .HasForeignKey(pt => pt.AdvertisementId);

            modelBuilder.Entity<AdvertisementAttribute>()
                .HasOne(pt => pt.Attribute)
                .WithMany(t => t.AdvertisementsAttributes)
                .HasForeignKey(pt => pt.AttributeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
