using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Menu> menu = _context.Menus.OrderBy(m => m.Order);

            return View("Default", menu);
        }

        public static void GetMenuList(IEnumerable<Menu> menu, ref string menuTree)
        {
            menuTree += "<ul class='nav navbar-nav'>";

            foreach (var item in menu)
            {
                menuTree += "<li class='nav-item dropdown'>";

                if (item.ParentId == null)
                {
                    menuTree += "<a href='" + item.Url + "' target='" + item.Target + "' class='nav-link'>" + item.Name + "</a>";

                    GetChildList(menu, ref menuTree, item.Id);
                }

                menuTree += "</li>";
            }

            menuTree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Menu> menu, ref string menuTree, int Id)
        {
            bool hasChild = false;
            string localMenuTree = "";

            localMenuTree += "<ul class='dropdown-menu bg-dark'>";

            foreach (var item in menu)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localMenuTree += "<li class='nav-item dropdown'>";
                    localMenuTree += "<a href='" + item.Url + "' target='" + item.Target + "' class='nav-link'>" + item.Name + "</a>";
                    GetChildList(menu, ref localMenuTree, item.Id);
                    localMenuTree += "</li>";
                }
            }

            localMenuTree += "</ul>";

            if (hasChild)
            {
                menuTree += localMenuTree;
            }
        }
    }
}
