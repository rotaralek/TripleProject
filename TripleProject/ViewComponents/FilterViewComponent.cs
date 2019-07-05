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

        public IViewComponentResult Invoke(string type = "", int minPrice = 0, int maxPrice = 0, int id = 0)
        {
            IEnumerable<Attribute> attribute = null;

            if (id == 0)
            {
                if (type == "Advertisement")
                {
                    attribute = (
                        from ad in _context.AdvertisementsAttributes
                        join at in _context.Attributes on ad.AttributeId equals at.Id into aaa
                        from a in aaa
                        select new Attribute
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Slug = a.Slug,
                            Description = a.Description,
                            Parent = a.Parent,
                            ParentId = a.ParentId
                        }
                    ).GroupBy(x => x.Id).Select(x => x.First()).OrderBy(a => a.Name);
                }
                else
                {
                    attribute = (
                        from ad in _context.ProductsAttributes
                        join at in _context.Attributes on ad.AttributeId equals at.Id into aaa
                        from a in aaa
                        select new Attribute
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Slug = a.Slug,
                            Description = a.Description,
                            Parent = a.Parent,
                            ParentId = a.ParentId
                        }
                    ).GroupBy(x => x.Id).Select(x => x.First()).OrderBy(a => a.Name);
                }
            }
            else
            {
                if (type == "Advertisement")
                {
                    attribute = (
                        from ac in _context.AdvertisementsCategories
                        join a in _context.Advertisements on ac.AdvertisementId equals a.Id into AdvertisementsCategoriesAdvertisements
                        where ac.CategoryId == id
                        from aca in AdvertisementsCategoriesAdvertisements.DefaultIfEmpty()
                        join aa in _context.AdvertisementsAttributes on aca.Id equals aa.AdvertisementId into AdvertisementsAttributesAdvertisements
                        from aaa in AdvertisementsAttributesAdvertisements.DefaultIfEmpty()
                        join a in _context.Attributes on aaa.AttributeId equals a.Id into AttributesList
                        from al in AttributesList.DefaultIfEmpty()
                        select new Attribute
                        {
                            Id = al.Id,
                            Name = al.Name,
                            Slug = al.Slug,
                            Description = al.Description,
                            Parent = al.Parent,
                            ParentId = al.ParentId
                        }
                    ).GroupBy(x => x.Id).Select(x => x.First()).OrderBy(a => a.Name);
                }
                else
                {
                    attribute = (
                        from pc in _context.ProductsCatalogs
                        join p in _context.Products on pc.ProductId equals p.Id into CatalogProducts
                        where pc.CatalogId == id
                        from cp in CatalogProducts
                        join pa in _context.ProductsAttributes on cp.Id equals pa.ProductId into PAttribures
                        from psa in PAttribures
                        join at in _context.Attributes on psa.AttributeId equals at.Id into aaa
                        from a in aaa
                        select new Attribute
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Slug = a.Slug,
                            Description = a.Description,
                            Parent = a.Parent,
                            ParentId = a.ParentId
                        }
                    ).GroupBy(x => x.Id).Select(x => x.First()).ToList();
                }
            }

            ViewData["type"] = type;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            ViewData["id"] = id;

            return View("Default", attribute);
        }

        public static void GetAttributesList(IEnumerable<Attribute> catalog, ref string catalogTree, IList<int> attributes = null)
        {
            foreach (var item in catalog)
            {
                if (item.ParentId == null)
                {
                    catalogTree += "<div class='nav-item card-body card mb-3 auto-parent-select'>";
                    catalogTree += "<p class='mb-1'>" + item.Name + "</p>";

                    GetChildList(catalog, ref catalogTree, item.Id, attributes);

                    catalogTree += "</div>";
                }
            }
        }

        public static void GetChildList(IEnumerable<Attribute> catalog, ref string catalogTree, int Id, IList<int> attributes = null)
        {
            bool hasChild = false;
            string localCatalogTree = "";

            localCatalogTree += "<ul class='nav flex-column sub-item pl-3'>";

            foreach (var item in catalog)
            {
                if (item.ParentId == Id)
                {
                    hasChild = true;

                    localCatalogTree += "<li class='nav-item''>";
                    bool isChecked = false;

                    if (attributes != null)
                    {
                        foreach (var selected in attributes)
                        {
                            if (item.Id == selected)
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (isChecked)
                    {
                        localCatalogTree += "<div class='form-check'><label><input type='checkbox' name='Attributes' value='" + item.Id + "' class='form-check-input' checked>" + item.Name + "</label></div>";
                    }
                    else
                    {
                        localCatalogTree += "<div class='form-check'><label><input type='checkbox' name='Attributes' value='" + item.Id + "' class='form-check-input'>" + item.Name + "</label></div>";
                    }

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
