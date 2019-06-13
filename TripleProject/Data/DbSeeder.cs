using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleProject.Areas.Admin.Models;

namespace TripleProject.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDbAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                await CreateUser(context, roleManager, userManager);
                await CreateCatalogs(context);
                await CreateCategories(context);
                await CreateProducts(context);
                await CreateAdvertisements(context);
            }
        }

        public static async Task CreateUser(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            //Create role
            IdentityRole role = new IdentityRole()
            {
                Name = "admin"
            };

            await roleManager.CreateAsync(role);

            //Create user
            IdentityUser user = new IdentityUser()
            {
                UserName = "a.rotari@tellus.md",
                Email = "a.rotari@tellus.md"
            };

            await userManager.CreateAsync(user, "Tellus!md1");

            await userManager.AddToRoleAsync(user, role.Name);
            await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(role.Name, "true"));
        }

        public static async Task CreateCatalogs(ApplicationDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                int? parentId = null;

                if (i == 4 || i == 6 || i == 9)
                {
                    parentId = 3;
                }

                Catalog catalog = new Catalog
                {
                    Slug = "attribute-" + i,
                    Name = "Catalog " + i,
                    ParentId = parentId
                };

                context.Add(catalog);

                await context.SaveChangesAsync();
            }
        }

        public static async Task CreateCategories(ApplicationDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                int? parentId = null;

                if (i == 3 || i == 5 || i == 7)
                {
                    parentId = 2;
                }

                Category category = new Category
                {
                    Slug = "category-" + i,
                    Name = "Category " + i,
                    ParentId = parentId
                };

                context.Add(category);

                await context.SaveChangesAsync();
            }
        }

        public static async Task CreateProducts(ApplicationDbContext context)
        {
            for (int i = 0; i < 20; i++) {
                Product product = new Product()
                {
                    Slug = "product-" + i,
                    Title = "Product " + i,
                    Text = "",
                    Description = "",
                    Price = 200,
                    Currency = 0,
                    Quantity = 150,
                    ImageId = null,
                    GalleryId = null,
                    DateTime = DateTime.Now
                };

                context.Add(product);

                await context.SaveChangesAsync();

                ProductCatalog productCatalog = new ProductCatalog()
                {
                    ProductId = product.Id,
                    CatalogId = 3
                };

                context.ProductsCatalogs.Add(productCatalog);

                await context.SaveChangesAsync();
            }
        }

        public static async Task CreateAdvertisements(ApplicationDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                Advertisement advertisement = new Advertisement()
                {
                    Slug = "advertisement-" + i,
                    Title = "Advertisement " + i,
                    Text = "",
                    Price = 200,
                    Currency = 0,
                    Quantity = 150,
                    ImageId = null,
                    GalleryId = null,
                    DateTime = DateTime.Now
                };

                context.Add(advertisement);

                await context.SaveChangesAsync();

                AdvertisementCategory advertisementCategory = new AdvertisementCategory()
                {
                    AdvertisementId = advertisement.Id,
                    CategoryId = 3
                };

                context.AdvertisementsCategories.Add(advertisementCategory);

                await context.SaveChangesAsync();
            }
        }
    }
}
