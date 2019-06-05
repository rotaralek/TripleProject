using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.ViewComponents
{
    public class CategoriesCheckboxTreeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoriesCheckboxTreeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(object advertisementsCategories = null)
        {
            ViewData["AdvertisementsCategories"] = advertisementsCategories;

            IEnumerable<Category> category = _context.Categories.OrderBy(m => m.Name);

            return View("Default", category);
        }

        public static void GetCatalogList(IEnumerable<Category> category, IEnumerable<AdvertisementCategory> advertisementCategory, ref string tree)
        {
            tree += "<ul class='nav navbar-nav sidebar-nav auto-parent-select'>";

            foreach (var item in category)
            {

                if (item.ParentId == null)
                {
                    tree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (advertisementCategory != null)
                    {
                        foreach (var selected in advertisementCategory)
                        {
                            if (item.Id == selected.CategoryId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsCategories' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsCategories' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(category, advertisementCategory, ref tree, item.Id);

                    tree += "</li>";
                }
            }

            tree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Category> category, IEnumerable<AdvertisementCategory> advertisementCategory, ref string tree, int Id)
        {
            bool hasChild = false;
            string localTree = "";

            localTree += "<ul class='sub-menu bg-white'>";

            foreach (var item in category)
            {

                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localTree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (advertisementCategory != null)
                    {
                        foreach (var selected in advertisementCategory)
                        {
                            if (item.Id == selected.CategoryId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsCategories' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsCategories' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(category, advertisementCategory, ref localTree, item.Id);
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
