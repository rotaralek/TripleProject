﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class ProductsCarouselViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProductsCarouselViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Product> products = _context.Products.Include(p => p.Image).OrderByDescending(p => p.Id);

            return View("Default", products);
        }
    }
}
