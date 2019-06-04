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
    public class SimilarProductsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SimilarProductsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int catalogId, int id)
        {
            IEnumerable<Product> products = _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == catalogId).Select(pc => pc.Product).Where(p => p.Id != id).Include(p => p.Image).OrderByDescending(p => p.Id).Take(5);

            return View("Default", products);
        }
    }
}
