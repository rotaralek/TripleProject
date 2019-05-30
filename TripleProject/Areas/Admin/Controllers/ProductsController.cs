using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.Products.CountAsync();
            var applicationDbContext = await _context.Products.Include(p => p.Attribute).Include(p => p.ProductsCatalogs).Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Attribute)
                .Include(p => p.ProductsCatalogs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["AttributeId"] = new SelectList(_context.ProductAttributes, "Id", "Name");

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,Description,ProductsCatalogs,Price,Currency,Quantity,AttributeId,ImageId,GalleryId,DateTime")] Product product, int[] ProductsCatalogs)
        {
            if (ModelState.IsValid)
            {
                product.DateTime = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();

                foreach (var item in ProductsCatalogs)
                {
                    ProductCatalog productCatalog = new ProductCatalog()
                    {
                        ProductId = product.Id,
                        CatalogId = item
                    };

                    _context.ProductsCatalogs.Add(productCatalog);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["AttributeId"] = new SelectList(_context.ProductAttributes, "Id", "Name", product.AttributeId);
            ViewData["ProductsCatalogs"] = await _context.ProductsCatalogs.Where(p => p.ProductId == id).ToListAsync();

            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,Description,ProductsCatalogs,Price,Currency,Quantity,AttributeId,ImageId,GalleryId,DateTime")] Product product, int[] ProductsCatalogs)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.DateTime == null)
                    {
                        product.DateTime = DateTime.Now;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    var productCatalogList = await _context.ProductsCatalogs.Where(p => p.ProductId == id).ToListAsync();

                    foreach (var item in productCatalogList)
                    {
                        var productCatalog = await _context.ProductsCatalogs.FirstOrDefaultAsync(p => p.ProductId == id);
                        _context.ProductsCatalogs.Remove(productCatalog);

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in ProductsCatalogs)
                    {
                        ProductCatalog productCatalog = new ProductCatalog()
                        {
                            ProductId = product.Id,
                            CatalogId = item
                        };

                        _context.ProductsCatalogs.Add(productCatalog);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
