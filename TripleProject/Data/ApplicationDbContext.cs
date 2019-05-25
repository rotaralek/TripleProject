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
        public DbSet<TripleProject.Areas.Admin.Models.Page> Pages { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Advertisement> Advertisements { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Product> Products { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.FileUpload> FileUploads { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Catalog> Catalogs { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Category> Categories { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.AdvertisementAttribute> AdvertisementAttributes { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.ProductAttribute> ProductAttributes { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Menu> Menus { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.ProductCatalog> ProductsCatalogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProductCatalog>().HasKey(pc => new { pc.ProductId, pc.CatalogId });

            //modelBuilder.Entity<Product>()
            //.HasOne(pc => pc.Catalogs)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Catalog>()
            //.HasOne(pc => pc.Products)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<ProductCatalog>()
            //.HasOne(bbt => bbt.Product)
            //.WithMany(b => b.ProductsCatalogs)
            //.HasForeignKey(bbt => bbt.ProductId);

            // modelBuilder.Entity<ProductCatalog>()
            //.HasOne(bbt => bbt.Catalog)
            //.WithMany(b => b.ProductsCatalogs)
            //.HasForeignKey(bbt => bbt.CatalogId);

            modelBuilder.Entity<Product>()
            .HasMany(c => c.Catalogs);

            modelBuilder.Entity<Catalog>()
            .HasMany(c => c.Products);

            base.OnModelCreating(modelBuilder);
        }
    }
}
