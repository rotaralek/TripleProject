﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Identity/Products
        public async Task<IActionResult> Index(int page = 1)
        {
            string userId = _userManager.GetUserId(User);
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.Products.Where(u => u.UserId == userId).CountAsync();
            var applicationDbContext = await _context.Products.Where(u => u.UserId == userId).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Identity/Products/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Identity/Products/Create
        public IActionResult Create()
        {
            string userId = _userManager.GetUserId(User);
            ViewData["Users"] = new SelectList(_context.Users.Where(u => u.Id == userId), "Id", "UserName");
            ViewData["Shops"] = new SelectList(_context.Shops.Where(u => u.UserId == userId), "Id", "Title");

            return View();
        }

        // POST: Identity/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Slug,Title,Text,Description,ProductsCatalogs,ProductsAttributes,Price,Currency,Quantity,AttributeId,ImageId,GalleryId,DateTime,UserId,ShopId")] Product product, int[] ProductsCatalogs, int[] ProductsAttributes)
        {
            if (ModelState.IsValid)
            {
                product.DateTime = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();

                //Catalogs
                foreach (var item in ProductsCatalogs)
                {
                    ProductCatalog productCatalog = new ProductCatalog()
                    {
                        ProductId = product.Id,
                        CatalogId = item
                    };

                    _context.ProductsCatalogs.Add(productCatalog);
                }

                //Attributes
                foreach (var item in ProductsAttributes)
                {
                    ProductAttribute productAttribute = new ProductAttribute()
                    {
                        ProductId = product.Id,
                        AttributeId = item
                    };

                    _context.ProductsAttributes.Add(productAttribute);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // GET: Identity/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["ProductsAttributes"] = await _context.ProductsAttributes.Where(p => p.ProductId == id).ToListAsync();
            ViewData["ProductsCatalogs"] = await _context.ProductsCatalogs.Where(p => p.ProductId == id).ToListAsync();
            ViewData["Users"] = new SelectList(_context.Users.Where(u => u.Id == userId), "Id", "UserName", product.User);
            ViewData["Shops"] = new SelectList(_context.Shops.Where(u => u.UserId == userId), "Id", "Title", product.ShopId);

            return View(product);
        }

        // POST: Identity/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Slug,Title,Text,Description,ProductsCatalogs,ProductsAttributes,Price,Currency,Quantity,AttributeId,ImageId,GalleryId,DateTime,UserId,ShopId")] Product product, int[] ProductsCatalogs, int[] ProductsAttributes)
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

                    //Catalogs
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

                    //Attributes
                    var productAttributeList = await _context.ProductsAttributes.Where(p => p.ProductId == id).ToListAsync();

                    foreach (var item in productAttributeList)
                    {
                        var productAttribute = await _context.ProductsAttributes.FirstOrDefaultAsync(p => p.ProductId == id);
                        _context.ProductsAttributes.Remove(productAttribute);

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in ProductsAttributes)
                    {
                        ProductAttribute productAttribute = new ProductAttribute()
                        {
                            ProductId = product.Id,
                            AttributeId = item
                        };

                        _context.ProductsAttributes.Add(productAttribute);
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

            ViewData["ProductsAttributes"] = await _context.ProductsAttributes.Where(p => p.ProductId == id).ToListAsync();
            ViewData["ProductsCatalogs"] = await _context.ProductsCatalogs.Where(p => p.ProductId == id).ToListAsync();

            return View(product);
        }

        // GET: Identity/Products/Delete/5
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

        // POST: Identity/Products/Delete/5
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
