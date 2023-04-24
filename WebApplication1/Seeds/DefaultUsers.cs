// using Microsoft.AspNetCore.Identity;
// using System.Security.Claims;
// using WebApplication1.Helpers;
// using WebApplication1.Models;

// namespace WebApplication1.Seeds
// {
//     public static class DefaultUsers
//     {
//         public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
//         {
//             //Seed Default User
//             var defaultUser = new ApplicationUser
//             {
//                 UserName = "basicuser@gmail.com",
//                 Email = "basicuser@gmail.com",
//                 EmailConfirmed = true,
//                 PhoneNumberConfirmed = true,
//             };
//             if (userManager.Users.All(u => u.Id != defaultUser.Id))
//             {
//                 var user = await userManager.FindByEmailAsync(defaultUser.Email);
//                 if (user == null)
//                 {
//                     await userManager.CreateAsync(defaultUser, "123Pa$$word!");
//                     await userManager.AddToRoleAsync(defaultUser, AppRoles.ClientCreator.ToString());
//                 }
//             }
//         }

//         // public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
//         // {
//         //     //Seed Default User
//         //     var defaultUser = new ApplicationUser
//         //     {
//         //         UserName = "superadmin@gmail.com",
//         //         Email = "superadmin@gmail.com",
//         //         EmailConfirmed = true,
//         //         PhoneNumberConfirmed = true,
//         //     };
//         //     if (userManager.Users.All(u => u.Id != defaultUser.Id))
//         //     {
//         //         var user = await userManager.FindByEmailAsync(defaultUser.Email);
//         //         if (user == null)
//         //         {
//         //             await userManager.CreateAsync(defaultUser, "123Pa$$word!");
//         //             await userManager.AddToRoleAsync(defaultUser, AppRoles.ClientCreator.ToString());
//         //             await userManager.AddToRoleAsync(defaultUser, AppRoles.ClientAdmin.ToString());
//         //             await userManager.AddToRoleAsync(defaultUser, AppRoles.PortalAdmin.ToString());
//         //         }
//         //         await roleManager.SeedClaimsForSuperAdmin();
//         //     }
//         // }

//         // private async static Task SeedClaimsForSuperAdmin(this RoleManager<ApplicationRole> roleManager)
//         // {
//         //     var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
//         //     await roleManager.AddPermissionClaim(adminRole, "Products");
//         // }

//         // public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string module)
//         // {
//         //     var allClaims = await roleManager.GetClaimsAsync(role);
//         //     var allPermissions = Permissions.GeneratePermissionsForModule(module);
//         //     foreach (var permission in allPermissions)
//         //     {
//         //         if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
//         //         {
//         //             await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
//         //         }
//         //     }
//         // }
//     }
// }