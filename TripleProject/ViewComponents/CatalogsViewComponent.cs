using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.ViewComponents
{
    public class CatalogsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CatalogsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Catalog> catalog = _context.Catalogs.OrderBy(m => m.Name);

            return View("Default", catalog);
        }

        public static void GetCatalogList(IEnumerable<Catalog> catalog, ref string catalogTree)
        {
            catalogTree += "<ul class='nav navbar-nav'>";

            foreach (var item in catalog)
            {

                if (item.ParentId == null)
                {
                    catalogTree += "<li class='nav-item border-bottom'>";
                    catalogTree += "<a href='/products/catalogs/" + item.Id + "/' class='nav-link'>" + item.Name + "</a>";

                    GetChildList(catalog, ref catalogTree, item.Id);

                    catalogTree += "</li>";
                }
            }

            catalogTree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Catalog> catalog, ref string catalogTree, int Id)
        {
            bool hasChild = false;
            string localCatalogTree = "";

            localCatalogTree += "<ul>";

            foreach (var item in catalog)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localCatalogTree += "<li class='nav-item'>";
                    localCatalogTree += "<a href='/products/catalogs/" + item.Id + "' class='nav-link'>" + item.Name + "</a>";
                    GetChildList(catalog, ref localCatalogTree, item.Id);
                    localCatalogTree += "</li>";
                }
            }

            localCatalogTree += "</ul>";

            if (hasChild)
            {
                catalogTree += localCatalogTree;
            }
        }
    }
}
