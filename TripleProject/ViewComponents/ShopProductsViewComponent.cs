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
    public class ShopProductsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ShopProductsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            IEnumerable<Catalog> catalogs = (from p in _context.Products
                      join pc in _context.ProductsCatalogs
                      on p.Id equals pc.ProductId
                      join c in _context.Catalogs
                      on pc.CatalogId equals c.Id
                      where p.ShopId == id
                      where c.ParentId == null
                      orderby c.Name
                      select new Catalog { Id = c.Id, Name = c.Name }).Distinct();

            ViewData["ShopId"] = id;

            return View("Default", catalogs);
        }
    }
}
