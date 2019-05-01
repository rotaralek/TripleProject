using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.ViewComponents
{
    public class ImageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ImageViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int ImageId)
        {
            IEnumerable<FileUpload> image = _context.FileUploads.Where(i => i.Id == ImageId).ToList();

            return View("Default", image);
        }
    }
}
