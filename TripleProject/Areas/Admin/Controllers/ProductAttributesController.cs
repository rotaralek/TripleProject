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
    public class ProductAttributesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductAttributesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductAttributes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductAttributes.ToListAsync());
        }

        // GET: Admin/ProductAttributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAttribute = await _context.ProductAttributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productAttribute == null)
            {
                return NotFound();
            }

            return View(productAttribute);
        }

        // GET: Admin/ProductAttributes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductAttributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ProductAttribute productAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productAttribute);
        }

        // GET: Admin/ProductAttributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAttribute = await _context.ProductAttributes.FindAsync(id);
            if (productAttribute == null)
            {
                return NotFound();
            }
            return View(productAttribute);
        }

        // POST: Admin/ProductAttributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductAttribute productAttribute)
        {
            if (id != productAttribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductAttributeExists(productAttribute.Id))
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
            return View(productAttribute);
        }

        // GET: Admin/ProductAttributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productAttribute = await _context.ProductAttributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productAttribute == null)
            {
                return NotFound();
            }

            return View(productAttribute);
        }

        // POST: Admin/ProductAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productAttribute = await _context.ProductAttributes.FindAsync(id);
            _context.ProductAttributes.Remove(productAttribute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductAttributeExists(int id)
        {
            return _context.ProductAttributes.Any(e => e.Id == id);
        }
    }
}
