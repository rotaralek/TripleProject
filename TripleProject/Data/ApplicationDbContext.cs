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
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AdvertisementAttribute> AdvertisementAttributes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ProductCatalog> ProductsCatalogs { get; set; }
        public DbSet<AdvertisementCategory> AdvertisementsCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
