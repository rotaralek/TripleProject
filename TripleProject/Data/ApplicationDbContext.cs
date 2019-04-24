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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TripleProject.Areas.Admin.Models.Page> Pages { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Advertisement> Advertisements { get; set; }
        public DbSet<TripleProject.Areas.Admin.Models.Product> Products { get; set; }
    }
}
