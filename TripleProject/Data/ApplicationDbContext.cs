using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;

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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
