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
    public class AdvertisementAttributesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementAttributesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdvertisementAttributes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdvertisementAttributes.ToListAsync());
        }

        // GET: Admin/AdvertisementAttributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementAttribute = await _context.AdvertisementAttributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementAttribute == null)
            {
                return NotFound();
            }

            return View(advertisementAttribute);
        }

        // GET: Admin/AdvertisementAttributes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdvertisementAttributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AdvertisementAttribute advertisementAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisementAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisementAttribute);
        }

        // GET: Admin/AdvertisementAttributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementAttribute = await _context.AdvertisementAttributes.FindAsync(id);
            if (advertisementAttribute == null)
            {
                return NotFound();
            }
            return View(advertisementAttribute);
        }

        // POST: Admin/AdvertisementAttributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AdvertisementAttribute advertisementAttribute)
        {
            if (id != advertisementAttribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisementAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementAttributeExists(advertisementAttribute.Id))
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
            return View(advertisementAttribute);
        }

        // GET: Admin/AdvertisementAttributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementAttribute = await _context.AdvertisementAttributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisementAttribute == null)
            {
                return NotFound();
            }

            return View(advertisementAttribute);
        }

        // POST: Admin/AdvertisementAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisementAttribute = await _context.AdvertisementAttributes.FindAsync(id);
            _context.AdvertisementAttributes.Remove(advertisementAttribute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementAttributeExists(int id)
        {
            return _context.AdvertisementAttributes.Any(e => e.Id == id);
        }
    }
}
