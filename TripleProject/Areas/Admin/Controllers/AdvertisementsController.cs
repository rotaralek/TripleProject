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
    public class AdvertisementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Advertisements
        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.Advertisements.CountAsync();
            var applicationDbContext = await _context.Advertisements.OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Admin/Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Slug,Title,Text,Price,Currency,Quantity,AdvertisementsCategories,AdvertisementsAttributes,AttributeId,ImageId,GalleryId,DateTime")] Advertisement advertisement, int[] AdvertisementsCategories, int[] AdvertisementsAttributes)
        {
            if (ModelState.IsValid)
            {
                advertisement.DateTime = DateTime.Now;
                _context.Add(advertisement);
                await _context.SaveChangesAsync();

                //Categories
                foreach (var item in AdvertisementsCategories)
                {
                    AdvertisementCategory advertisementCategory = new AdvertisementCategory()
                    {
                        AdvertisementId = advertisement.Id,
                        CategoryId = item
                    };

                    _context.AdvertisementsCategories.Add(advertisementCategory);
                }

                //Attributes
                foreach (var item in AdvertisementsAttributes)
                {
                    AdvertisementAttribute advertisementAttribute = new AdvertisementAttribute()
                    {
                        AdvertisementId = advertisement.Id,
                        AttributeId = item
                    };

                    _context.AdvertisementsAttributes.Add(advertisementAttribute);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: Admin/Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            ViewData["AdvertisementsAttributes"] = await _context.AdvertisementsAttributes.Where(a => a.AdvertisementId == id).ToListAsync();
            ViewData["AdvertisementsCategories"] = await _context.AdvertisementsCategories.Where(a => a.AdvertisementId == id).ToListAsync();

            return View(advertisement);
        }

        // POST: Admin/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Slug,Title,Text,Price,Currency,Quantity,AdvertisementsCategories,AdvertisementsAttributes,AttributeId,ImageId,GalleryId,DateTime")] Advertisement advertisement, int[] AdvertisementsCategories, int[] AdvertisementsAttributes)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (advertisement.DateTime == null)
                    {
                        advertisement.DateTime = DateTime.Now;
                    }

                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();

                    //Categories
                    var advertisementCategoryList = await _context.AdvertisementsCategories.Where(p => p.AdvertisementId == id).ToListAsync();

                    foreach (var item in advertisementCategoryList)
                    {
                        var advertisementCategory = await _context.AdvertisementsCategories.FirstOrDefaultAsync(p => p.AdvertisementId == id);
                        _context.AdvertisementsCategories.Remove(advertisementCategory);

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in AdvertisementsCategories)
                    {
                        AdvertisementCategory advertisementCategory = new AdvertisementCategory()
                        {
                            AdvertisementId = advertisement.Id,
                            CategoryId = item
                        };

                        _context.AdvertisementsCategories.Add(advertisementCategory);
                    }

                    //Attributes
                    var advertisementAttributeList = await _context.AdvertisementsAttributes.Where(p => p.AdvertisementId == id).ToListAsync();

                    foreach (var item in advertisementAttributeList)
                    {
                        var advertisementAttribute = await _context.AdvertisementsAttributes.FirstOrDefaultAsync(p => p.AdvertisementId == id);
                        _context.AdvertisementsAttributes.Remove(advertisementAttribute);

                        await _context.SaveChangesAsync();
                    }

                    foreach (var item in AdvertisementsAttributes)
                    {
                        AdvertisementAttribute advertisementAttribute = new AdvertisementAttribute()
                        {
                            AdvertisementId = advertisement.Id,
                            AttributeId = item
                        };

                        _context.AdvertisementsAttributes.Add(advertisementAttribute);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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

            return View(advertisement);
        }

        // GET: Admin/Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
