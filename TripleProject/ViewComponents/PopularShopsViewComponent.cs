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
    public class PopularShopsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PopularShopsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Shop> shops = _context.Shops.Include(p => p.Image).OrderByDescending(p => p.Views).Take(5);

            return View("Default", shops);
        }
    }
}
