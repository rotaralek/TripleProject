using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;
using TripleProject.Data;
using Attribute = TripleProject.Areas.Admin.Models.Attribute;

namespace TripleProject.Areas.Admin.ViewComponents
{
    public class AdvertisementsAttributesCheckboxTreeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementsAttributesCheckboxTreeViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(object attributes = null)
        {
            ViewData["Attributes"] = attributes;

            IEnumerable<Attribute> attribute = _context.Attributes.OrderBy(m => m.Name);

            return View("Default", attribute);
        }

        public static void GetAttributeList(IEnumerable<Attribute> attribute, IEnumerable<AdvertisementAttribute> advertisementAttribute, ref string tree)
        {
            tree += "<ul class='nav navbar-nav sidebar-nav auto-parent-select'>";

            foreach (var item in attribute)
            {
                if (item.ParentId == null)
                {
                    tree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (advertisementAttribute != null)
                    {
                        foreach (var selected in advertisementAttribute)
                        {
                            if (item.Id == selected.AttributeId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsAttributes' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        tree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsAttributes' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(attribute, advertisementAttribute, ref tree, item.Id);

                    tree += "</li>";
                }
            }

            tree += "</ul>";
        }

        public static void GetChildList(IEnumerable<Attribute> attribute, IEnumerable<AdvertisementAttribute> advertisementAttribute, ref string tree, int Id)
        {
            bool hasChild = false;
            string localTree = "";

            localTree += "<ul class='sub-item'>";

            foreach (var item in attribute)
            {
                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localTree += "<li class='nav-item'>";

                    bool isChecked = false;

                    if (advertisementAttribute != null)
                    {
                        foreach (var selected in advertisementAttribute)
                        {
                            if (item.Id == selected.AttributeId)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsAttributes' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        localTree += "<div class='form-check'><label><input type='checkbox' name='AdvertisementsAttributes' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

                    GetChildList(attribute, advertisementAttribute, ref localTree, item.Id);
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
