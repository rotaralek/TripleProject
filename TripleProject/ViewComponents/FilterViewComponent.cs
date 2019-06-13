using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;
using Attribute = TripleProject.Areas.Admin.Models.Attribute;

namespace TripleProject.ViewComponents
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FilterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Attribute> attribute = _context.Attributes.OrderBy(m => m.Name);

            return View("Default", attribute);
        }

        public static void GetAttributesList(IEnumerable<Attribute> catalog, ref string catalogTree)
        {
            foreach (var item in catalog)
            {

                if (item.ParentId == null)
                {
                    catalogTree += "<div class='nav-item card-body card mb-3 auto-parent-select'>";
                    catalogTree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";

                    GetChildList(catalog, ref catalogTree, item.Id);

                    catalogTree += "</div>";
                }
            }
        }

        public static void GetChildList(IEnumerable<Attribute> catalog, ref string catalogTree, int Id)
        {
            bool hasChild = false;
            string localCatalogTree = "";

            localCatalogTree += "<ul class='nav flex-column sub-item pl-3'>";

            foreach (var item in catalog)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localCatalogTree += "<li class='nav-item'>";
                    localCatalogTree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";

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
