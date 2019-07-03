using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Controllers;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PaginationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int count, int page, decimal itemsPerPage, string type = "", int minPrice = 0, int maxPrice = 12000, List<int> productsCatalogs = null)
        {
            int pages;
            decimal totalPages;

            ViewData["type"] = type;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["productsCatalogs"] = productsCatalogs;

            if (count == 0)
            {
                ViewData["count"] = 0;
                ViewData["page"] = 1;
                ViewData["pages"] = 1;

                return View();
            }

            totalPages = count / itemsPerPage;
            pages = (int)Math.Ceiling(totalPages);

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["pages"] = pages;

            return View();
        }
    }
}
