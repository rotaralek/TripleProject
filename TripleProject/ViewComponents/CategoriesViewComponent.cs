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
            categoryTree += "<ul class='nav navbar-nav sidebar-nav'>";

            foreach (var item in category)
            {

                if (item.ParentId == null)
                {
                    categoryTree += "<li class='nav-item border-bottom'>";
                    categoryTree += "<a href='/advertisements/categories/" + item.Id + "/' class='nav-link'>" + item.Name + "</a>";

                    GetChildList(category, ref categoryTree, item.Id);

                    categoryTree += "</li>";
                }
            }

            categoryTree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Category> category, ref string categoryTree, int Id)
        {
            bool hasChild = false;
            string localCategoryTree = "";

            localCategoryTree += "<i class='material-icons'>keyboard_arrow_right</i><ul class='sub-menu bg-white'>";

            foreach (var item in category)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localCategoryTree += "<li class='nav-item border-bottom'>";
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
