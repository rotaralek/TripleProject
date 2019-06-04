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
    public class SimilarAdvertisementsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SimilarAdvertisementsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int categoryId, int id)
        {
            IEnumerable<Advertisement> advertisements = _context.Set<AdvertisementCategory>().Where(pc => pc.CategoryId == categoryId).Select(pc => pc.Advertisement).Where(p => p.Id != id).Include(p => p.Image).OrderByDescending(p => p.Id).Take(5);

            return View("Default", advertisements);
        }
    }
}
