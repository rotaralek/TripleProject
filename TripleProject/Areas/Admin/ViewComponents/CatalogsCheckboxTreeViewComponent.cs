using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.ViewComponents
{
    public class CatalogsCheckboxTreeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CatalogsCheckboxTreeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(object productsCatalogs = null)
        {
            ViewData["ProductsCatalogs"] = productsCatalogs;

            IEnumerable<Catalog> catalog = _context.Catalogs.OrderBy(m => m.Name);

            return View("Default", catalog);
        }

        public static void GetCatalogList(IEnumerable<Catalog> catalog, IEnumerable<ProductCatalog> productCatalog, ref string tree)
        {
            tree += "<ul class='nav navbar-nav sidebar-nav auto-parent-select'>";

            foreach (var item in catalog)
            {

                if (item.ParentId == null)
                {
                    tree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (productCatalog != null)
                    {
                        foreach (var selected in productCatalog)
                        {
                            if (item.Id == selected.CatalogId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(catalog, productCatalog, ref tree, item.Id);

                    tree += "</li>";
                }
            }

            tree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Catalog> catalog, IEnumerable<ProductCatalog> productCatalog, ref string tree, int Id)
        {
            bool hasChild = false;
            string localTree = "";

            localTree += "<ul class='sub-menu bg-white'>";

            foreach (var item in catalog)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localTree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (productCatalog != null)
                    {
                        foreach (var selected in productCatalog)
                        {
                            if (item.Id == selected.CatalogId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='ProductsCatalogs' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(catalog, productCatalog, ref localTree, item.Id);
                    localTree += "</li>";
                }
            }

            localTree += "</ul>";

            if (hasChild)
            {
                tree += localTree;
            }
        }
    }
}
