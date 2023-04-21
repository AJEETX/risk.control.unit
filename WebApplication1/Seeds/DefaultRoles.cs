using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.PortalAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.ClientAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.ClientCreator.ToString()));
        }
    }
}