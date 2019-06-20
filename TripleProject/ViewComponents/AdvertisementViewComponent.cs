using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;

namespace TripleProject.ViewComponents
{
    public class AdvertisementViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Advertisement advertisement)
        {
            return View("Default", advertisement);
        }
    }
}