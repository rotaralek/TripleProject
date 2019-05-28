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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
