﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AdvertisementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdvertisementsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Identity/Advertisements
        public async Task<IActionResult> Index(int page = 1)
        {
            string userId = _userManager.GetUserId(User);
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.Advertisements.Where(u => u.UserId == userId).CountAsync();
            var applicationDbContext = await _context.Advertisements.Where( u => u.UserId == userId).Include(p => p.Image).OrderByDescending(p => p.DateTime).Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Identity/Advertisements/Details/5
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

        // GET: Identity/Advertisements/Create
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.Users, "Id", "UserName");

            return View();
        }

        // POST: Identity/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Slug,Title,Text,Price,Currency,Quantity,AdvertisementsCategories,AdvertisementsAttributes,ImageId,GalleryId,DateTime,UserId")] Advertisement advertisement, int[] AdvertisementsCategories, int[] AdvertisementsAttributes)
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

            ViewData["Users"] = new SelectList(_context.Users, "Id", "UserName", advertisement.UserId);

            return View(advertisement);
        }

        // GET: Identity/Advertisements/Edit/5
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
            ViewData["Users"] = new SelectList(_context.Users, "Id", "UserName", advertisement.UserId);

            return View(advertisement);
        }

        // POST: Identity/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Slug,Title,Text,Price,Currency,Quantity,AdvertisementsCategories,AdvertisementsAttributes,ImageId,GalleryId,DateTime,UserId")] Advertisement advertisement, int[] AdvertisementsCategories, int[] AdvertisementsAttributes)
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

            ViewData["AdvertisementsAttributes"] = await _context.AdvertisementsAttributes.Where(a => a.AdvertisementId == id).ToListAsync();
            ViewData["AdvertisementsCategories"] = await _context.AdvertisementsCategories.Where(a => a.AdvertisementId == id).ToListAsync();
            ViewData["Users"] = new SelectList(_context.Users, "Id", "UserName", advertisement.UserId);

            return View(advertisement);
        }

        // GET: Identity/Advertisements/Delete/5
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

        // POST: Identity/Advertisements/Delete/5
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
