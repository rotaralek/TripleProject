using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Controllers;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.ViewComponents
{
    public class GalleryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public GalleryViewComponent(ApplicationDbContext context, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public IViewComponentResult Invoke(string GalleryId = null)
        {
            List<int> galleryList = new List<int>();

            if (GalleryId != null)
            {
                galleryList = GalleryId.Split(";").Select(Int32.Parse).ToList();
            }

            IEnumerable<FileUpload> gallery = _context.FileUploads.Where(i => galleryList.Contains(i.Id)).ToList();

            ViewData["Gallery"] = _sharedLocalizer["Gallery"];
            ViewData["ClickToUpload"] = _sharedLocalizer["Click to upload images"];
            ViewData["Loading"] = _sharedLocalizer["Loading..."];

            return View("Default", gallery);
        }
    }
}
