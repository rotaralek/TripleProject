using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> category = _context.Categories.OrderBy(m => m.Name);

            return View("Default", category);
        }

        public static void GetCategoryList(IEnumerable<Category> category, ref string categoryTree)
        {
            categoryTree += "<ul class='nav navbar-nav'>";

            foreach (var item in category)
            {
                categoryTree += "<li class='nav-item border-bottom'>";

                if (item.ParentId == null)
                {
                    categoryTree += "<a href='/advertisements/categories/" + item.Id + "/' class='nav-link'>" + item.Name + "</a>";

                    GetChildList(category, ref categoryTree, item.Id);
                }

                categoryTree += "</li>";
            }

            categoryTree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Category> category, ref string categoryTree, int Id)
        {
            bool hasChild = false;
            string localCategoryTree = "";

            localCategoryTree += "<ul>";

            foreach (var item in category)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localCategoryTree += "<li class='nav-item'>";
                    localCategoryTree += "<a href='/advertisements/categories/" + item.Id + "' class='nav-link'>" + item.Name + "</a>";
                    GetChildList(category, ref localCategoryTree, item.Id);
                    localCategoryTree += "</li>";
                }
            }

            localCategoryTree += "</ul>";

            if (hasChild)
            {
                categoryTree += localCategoryTree;
            }
        }
    }
}
