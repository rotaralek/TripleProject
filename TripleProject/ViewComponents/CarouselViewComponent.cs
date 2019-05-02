using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CarouselViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string GalleryId)
        {
            List<int> galleryList = new List<int>();

            if (GalleryId != "")
            {
                galleryList = GalleryId.Split(";").Select(Int32.Parse).ToList();
            }

            IEnumerable<FileUpload> gallery = _context.FileUploads.Where(i => galleryList.Contains(i.Id)).ToList();

            return View("Default", gallery);
        }
    }
}
