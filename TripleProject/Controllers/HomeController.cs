using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;
using TripleProject.Models;
using Attribute = TripleProject.Areas.Admin.Models.Attribute;

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
                .FirstOrDefaultAsync(m => m.Slug == id);
            if (page == null)
            {
                return NotFound();
            }

            return View("Pages/Single", page);
        }

        [Route("shops")]
        public async Task<IActionResult> ShopsArchive(int page = 1)
        {
            int itemsPerPage = 12;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.Shops.CountAsync();
            var applicationDbContext = await _context.Shops.Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View("Shops/Archive", applicationDbContext);
        }

        [Route("shops/{id?}")]
        public async Task<IActionResult> ShopsSingle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Shops
                .Include(p => p.Image)
                //.Include(p => p.Products)
                //.ThenInclude(i => i.Image)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View("Shops/Single", product);
        }

        [Route("products")]
        public async Task<IActionResult> ProductsArchive(int page = 1, string type = "", int minPrice = 0, int maxPrice = 12000, IList<int> attributes = null)
        {
            int itemsPerPage = 12;
            int skip = itemsPerPage * (page - 1);
            int count = 0;
            IEnumerable<Product> applicationDbContext = null;

            if (type == "")
            {
                count = await _context.Products.CountAsync();
                applicationDbContext = await _context.Products.Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else if (attributes.Count() == 0)
            {
                count = await _context.Products.Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Products.Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else
            {
                count = await _context.Set<ProductAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Product).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Set<ProductAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Product).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }

            decimal totalPages = count / itemsPerPage;
            int pages = (int)Math.Ceiling(totalPages);

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;
            ViewData["pages"] = pages;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["attributes"] = attributes;

            return View("Products/Archive", applicationDbContext);
        }

        [Route("products/catalogs/{id?}")]
        public async Task<IActionResult> ProductsCatalog(int? id, int page = 1, string type = "", int minPrice = 0, int maxPrice = 12000, IList<int> attributes = null)
        {
            int itemsPerPage = 12;
            int skip = itemsPerPage * (page - 1);
            int count = 0;
            IEnumerable<Product> applicationDbContext = null;

            if (type == "")
            {
                count = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == id).Select(pc => pc.Product).CountAsync();
                applicationDbContext = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == id).Select(pc => pc.Product).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else if (attributes.Count() == 0)
            {
                count = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == id).Select(pc => pc.Product).Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == id).Select(pc => pc.Product).Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else
            {
                count = await _context.Set<ProductAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Product).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                //applicationDbContext = await _context.Set<ProductAttribute>().Where(pa => productsAttributes.Contains(pa.AttributeId)).Select(pc => pc.Product).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
                applicationDbContext = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == id).Select(pc => pc.Product).Include(p => p.ProductsAttributes).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            }

            decimal totalPages = count / itemsPerPage;
            int pages = (int)Math.Ceiling(totalPages);

            ViewData["id"] = id;
            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;
            ViewData["pages"] = pages;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["attributes"] = attributes;

            return View("Products/Catalog", applicationDbContext);
        }

        [Route("products/{id?}")]
        public async Task<IActionResult> ProductsSingle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductsAttributes)
                .Include(p => p.ProductsCatalogs)
                .Include(p => p.Image)
                .Include(p => p.Shops)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            IEnumerable<Catalog> catalog = await _context.Catalogs.ToListAsync();
            ViewData["Catalogs"] = catalog;

            IEnumerable<Attribute> attribute = await _context.Attributes.ToListAsync();
            ViewData["Attributes"] = attribute;

            return View("Products/Single", product);
        }

        [Route("advertisements")]
        public async Task<IActionResult> AdvertisementsArchive(int page = 1, string type = "", int minPrice = 0, int maxPrice = 12000, IList<int> attributes = null)
        {
            int itemsPerPage = 12;
            int skip = itemsPerPage * (page - 1);
            int count = 0;            
            IEnumerable<Advertisement> applicationDbContext = null;

            if (type == "")
            {
                count = await _context.Advertisements.CountAsync();
                applicationDbContext = await _context.Advertisements.Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else if (attributes.Count() == 0)
            {
                count = await _context.Advertisements.Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Advertisements.Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else
            {
                count = await _context.Set<AdvertisementAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Advertisement).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Set<AdvertisementAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Advertisement).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }

            decimal totalPages = count / itemsPerPage;
            int pages = (int)Math.Ceiling(totalPages);

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;
            ViewData["pages"] = pages;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["attributes"] = attributes;

            return View("Advertisements/Archive", applicationDbContext);
        }

        [Route("advertisements/{id?}")]
        public async Task<IActionResult> AdvertisementsSingle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(p => p.AdvertisementsAttributes)
                .Include(p => p.AdvertisemetsCategories)
                .Include(p => p.Image)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            IEnumerable<Category> category = await _context.Categories.ToListAsync();
            ViewData["Categories"] = category;

            IEnumerable<Attribute> attribute = await _context.Attributes.ToListAsync();
            ViewData["Attributes"] = attribute;

            return View("Advertisements/Single", advertisement);
        }

        [Route("advertisements/categories/{id?}")]
        public async Task<IActionResult> AdvertisementsCategory(int? id, int page = 1, string type = "", int minPrice = 0, int maxPrice = 12000, IList<int> attributes = null)
        {
            int itemsPerPage = 12;
            int skip = itemsPerPage * (page - 1);
            int count = 0;
            IEnumerable<Advertisement> applicationDbContext = null;
                       
            if (type == "")
            {
                count = await _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == id).Select(pc => pc.Advertisement).CountAsync();
                applicationDbContext = await _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == id).Select(pc => pc.Advertisement).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else if (attributes.Count() == 0)
            {
                count = await _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == id).Select(pc => pc.Advertisement).Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                applicationDbContext = await _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == id).Select(pc => pc.Advertisement).Include(p => p.Image).Where(p => p.Price >= minPrice && p.Price <= maxPrice).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
            }
            else
            {
                count = await _context.Set<AdvertisementAttribute>().Where(pa => attributes.Contains(pa.AttributeId)).Select(pc => pc.Advertisement).Where(p => p.Price >= minPrice && p.Price <= maxPrice).CountAsync();
                //applicationDbContext = await _context.Set<ProductAttribute>().Where(pa => productsAttributes.Contains(pa.AttributeId)).Select(pc => pc.Product).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();
                applicationDbContext = await _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == id).Select(pc => pc.Advertisement).Include(p => p.AdvertisementsAttributes).Where(p => p.Price >= minPrice && p.Price <= maxPrice).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            }

            decimal totalPages = count / itemsPerPage;
            int pages = (int)Math.Ceiling(totalPages);

            ViewData["id"] = id;
            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;
            ViewData["pages"] = pages;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["attributes"] = attributes;

            return View("Advertisements/Category", applicationDbContext);
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
                case "shop":
                    Shop shop = await _context.Shops.FindAsync(id);

                    if (shop.Views != null)
                    {
                        shop.Views = shop.Views + 1;
                    }
                    else
                    {
                        shop.Views = 1;
                    }

                    response.Add(shop.Views);
                    _context.Update(shop);
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

        [Route("AjaxCatalogProductsLoad")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxCatalogProductsLoad(int shopId, int catalogId)
        {
            int itemsPerPage = 12;
            int skip = 0;
            var applicationDbContext = await _context.Set<ProductCatalog>().Where(pc => pc.CatalogId == catalogId).Select(pc => pc.Product).Where(pc => pc.ShopId == shopId).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            return new JsonResult(applicationDbContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
