using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Models.ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public List<UsersViewModel> UserList;
        private readonly ApplicationDbContext context;

        public UserController(UserManager<Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            UserList = new List<UsersViewModel>();
        }
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            foreach (Models.ApplicationUser user in users)
            {
                var thisViewModel = new UsersViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                UserList.Add(thisViewModel);
            }
            return View(UserList);
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string userId)
        {

            if (userId == null)
            {

                return NotFound();
            }

            var applicationUser = await context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == userId);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //super admin should always have access to Roles
                    applicationUser.ApplicationUserRole = applicationUser.isSuperAdmin ? true : applicationUser.ApplicationUserRole;

                    context.ApplicationUser.Update(applicationUser);
                    await context.SaveChangesAsync();                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }
        private async Task<List<string>> GetUserRoles(Models.ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }
    }
}
