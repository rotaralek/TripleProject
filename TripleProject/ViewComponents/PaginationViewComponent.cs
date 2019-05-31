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
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public PaginationViewComponent(ApplicationDbContext context, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public IViewComponentResult Invoke(int count, int page, decimal itemsPerPage)
        {
            int pages;
            decimal totalPages;

            if (count == 0)
            {
                ViewData["count"] = 0;
                return View();
            }

            totalPages = count / itemsPerPage;
            pages = (int)Math.Ceiling(totalPages);

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["pages"] = pages;

            ViewData["_Page"] = _sharedLocalizer["Page"];
            ViewData["_From"] = _sharedLocalizer["from"];
            ViewData["_First"] = _sharedLocalizer["First"];
            ViewData["_Previous"] = _sharedLocalizer["Previous"];
            ViewData["_CurrentPage"] = _sharedLocalizer["Current page"];
            ViewData["_Next"] = _sharedLocalizer["Next"];
            ViewData["_Last"] = _sharedLocalizer["Last"];

            return View();
        }
    }
}
