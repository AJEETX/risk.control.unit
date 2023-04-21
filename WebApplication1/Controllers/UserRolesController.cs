using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class UserRolesController : Controller
    {
    private readonly SignInManager<Models.ApplicationUser> signInManager;
        private readonly UserManager<Models.ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IToastNotification toastNotification;

        public UserRolesController(UserManager<Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<Models.ApplicationUser> signInManager, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.toastNotification = toastNotification;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index(string userId)
        {
            var userRoles = new List<UserRoleViewModel>();
            //ViewBag.userId = userId;
            Models.ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            //ViewBag.UserName = user.UserName;
            foreach (var role in roleManager.Roles)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.Selected = true;
                }
                else
                {
                    userRoleViewModel.Selected = false;
                }
                userRoles.Add(userRoleViewModel);
            }
            var model = new UserRolesViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                UserRoleViewModel = userRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            result = await userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await userManager.GetUserAsync(User);
            await signInManager.RefreshSignInAsync(currentUser);


            await SeedSuperAdminAsync(userManager, roleManager);
            return RedirectToAction("Index", new { userId = id });
        }
        private static async Task SeedSuperAdminAsync(UserManager<Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new Models.ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, AppRoles.ClientCreator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, AppRoles.ClientAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, AppRoles.PortalAdmin.ToString());
                }
                await SeedClaimsForSuperAdmin(roleManager);
            }
        }
        private async static Task SeedClaimsForSuperAdmin(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await AddPermissionClaim(roleManager, adminRole, "Products");
        }

        public static async Task AddPermissionClaim(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
