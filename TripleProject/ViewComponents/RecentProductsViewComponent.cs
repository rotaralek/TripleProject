using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class RecentProductsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RecentProductsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Product> products = _context.Products.Include(p => p.Image).OrderByDescending(p => p.DateTime).Take(5);

            return View("Default", products);
        }
    }
}
