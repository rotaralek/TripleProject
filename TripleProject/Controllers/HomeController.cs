using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;
using TripleProject.Models;
using TripleProject.ViewModels;

namespace TripleProject.Controllers
{
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
            var applicationDbContext = _context.Products.Include(p => p.Attribute).Include(p => p.Catalog).Include(p => p.Image);
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
        public async Task<IActionResult> AdvertisementsArchive(int page = 1)
        {
            int pageSize = 3;

            IQueryable<Advertisement> source = _context.Advertisements.Include(p => p.Attribute).Include(p => p.Category).Include(p => p.Image);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Advertisements = items
            };

            return View("Advertisements/Archive", viewModel);
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
        public IActionResult AdvertisementsCategory(int id)
        {
            var applicationDbContext = _context.Advertisements.Include(p => p.Attribute).Include(p => p.Category).Include(p => p.Image).Where(p => p.CategoryId == id);
            IndexViewModel viewModel = new IndexViewModel
            {
                Advertisements = applicationDbContext
            };

            return View("Advertisements/Category",  viewModel);
        }

        [Route("products/catalogs/{id?}")]
        public IActionResult ProductsCatalog(int id)
        {
            var applicationDbContext = _context.Products.Include(p => p.Attribute).Include(p => p.Catalog).Include(p => p.Image).Where(p => p.CatalogId == id);
            IndexViewModel viewModel = new IndexViewModel
            {
                Products = applicationDbContext
            };

            return View("Products/Catalog", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
