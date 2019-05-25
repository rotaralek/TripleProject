using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleProject.Data
{
    public static class DbSeeder
    {
        public static void SeedDb(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            IdentityRole role = new IdentityRole()
            {
                Name = "admin"
            };

            roleManager.CreateAsync(role).Wait();

            IdentityUser user = new IdentityUser()
            {
                UserName = "a.rotari@tellus.md",
                Email = "a.rotari@tellus.md"
            };

            userManager.CreateAsync(user, "Tellus!md1").Wait();

            userManager.AddToRoleAsync(user, role.Name).Wait();
            userManager.AddClaimAsync(user, new System.Security.Claims.Claim(role.Name, "true"));
        }
    }
}
