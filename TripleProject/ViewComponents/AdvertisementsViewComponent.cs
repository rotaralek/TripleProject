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
    public class AdvertisementsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Advertisement> advertisements = _context.Advertisements.Include(p => p.Image).OrderByDescending(p => p.Id).Take(5);

            return View("Default", advertisements);
        }
    }
}
