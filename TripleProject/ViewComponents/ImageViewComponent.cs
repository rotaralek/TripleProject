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
    public class ImageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ImageViewComponent(ApplicationDbContext context, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
        }

        public IViewComponentResult Invoke(int ImageId)
        {
            IEnumerable<FileUpload> image = _context.FileUploads.Where(i => i.Id == ImageId).ToList();

            ViewData["Image"] = _sharedLocalizer["Image"];
            ViewData["ClickToUpload"] = _sharedLocalizer["Click to upload one image"];
            ViewData["Loading"] = _sharedLocalizer["Loading..."];

            return View("Default", image);
        }
    }
}
