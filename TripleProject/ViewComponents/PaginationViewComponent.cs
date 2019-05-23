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
    public class PaginationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PaginationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string type, int page, int itemsPerPage)
        {
            int itemsCount;
            int pages;
            decimal totalPages;


            switch (type)
            {
                case "product":
                    itemsCount = _context.Products.Count();
                    break;
                case "advertisement":
                    itemsCount = _context.Advertisements.Count();
                    break;
                default:
                    itemsCount = 0;
                    break;
            }

            totalPages = itemsCount / itemsPerPage;
            pages = (int)Math.Ceiling(totalPages) + 1;

            ViewData["page"] = page;
            ViewData["pages"] = pages;
            ViewData["type"] = type;

            return View();
        }
    }
}
