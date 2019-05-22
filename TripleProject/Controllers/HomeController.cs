using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;
using TripleProject.Models;

namespace TripleProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/{id?}")]
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.Title == id);
            if (page == null)
            {
                return NotFound();
            }

            return View("Pages/Single", page);
        }

        [Route("products")]
        public async Task<IActionResult> ProductsArchive()
        {
            var applicationDbContext = _context.Products.Include(p => p.Attribute).Include(p => p.Catalog).Include(p => p.Image).OrderByDescending(p => p.Id);

            return View("Products/Archive", await applicationDbContext.ToListAsync());
        }

        [Route("products/{id?}")]
        public async Task<IActionResult> ProductsSingle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Attribute)
                .Include(p => p.Catalog)
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("Products/Single", product);
        }

        [Route("advertisements")]
        public async Task<IActionResult> AdvertisementsArchive()
        {
            var applicationDbContext = _context.Advertisements.Include(p => p.Attribute).Include(p => p.Category).Include(p => p.Image).OrderByDescending(p => p.Id); ;

            return View("Advertisements/Archive", await applicationDbContext.ToListAsync());
        }

        [Route("advertisements/{id?}")]
        public async Task<IActionResult> AdvertisementsSingle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(p => p.Attribute)
                .Include(p => p.Category)
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View("Advertisements/Single", advertisement);
        }

        [Route("advertisements/categories/{id?}")]
        public async Task<IActionResult> AdvertisementsCategory(int? id)
        {
            var applicationDbContext = _context.Advertisements.Include(p => p.Attribute).Include(p => p.Category).Include(p => p.Image).Where(p => p.CategoryId == id).OrderByDescending(p => p.Id); ;

            return View("Advertisements/Category", await applicationDbContext.ToListAsync());
        }

        [Route("products/catalogs/{id?}")]
        public async Task<IActionResult> ProductsCatalog(int? id)
        {
            var applicationDbContext = _context.Products.Include(p => p.Attribute).Include(p => p.Catalog).Include(p => p.Image).Where(p => p.CatalogId == id).OrderByDescending(p => p.Id); ;

            return View("Products/Catalog", await applicationDbContext.ToListAsync());
        }

        [Route("AjaxViewsIncrement")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxViewsIncrement(string type, int id)
        {
            List<Object> response = new List<Object>();

            switch (type)
            {
                case "product":
                    Product product = await _context.Products.FindAsync(id);

                    if (product.Views != null)
                    {
                        product.Views = product.Views + 1;
                    }
                    else
                    {
                        product.Views = 1;
                    }

                    response.Add(product.Views);
                    _context.Update(product);
                    break;
                case "advertisement":
                    Advertisement advertisement = await _context.Advertisements.FindAsync(id);

                    if (advertisement.Views != null)
                    {
                        advertisement.Views = advertisement.Views + 1;
                    }
                    else
                    {
                        advertisement.Views = 1;
                    }

                    response.Add(advertisement.Views);
                    _context.Update(advertisement);
                    break;
                default:
                    response.Add(type);
                    response.Add(id);
                    response.Add("Error");
                    break;
            }

            await _context.SaveChangesAsync();

            return new JsonResult(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
