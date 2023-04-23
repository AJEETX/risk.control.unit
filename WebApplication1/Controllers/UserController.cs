using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        public List<UsersViewModel> UserList;
        private readonly ApplicationDbContext context;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public UserController(UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.passwordHasher = passwordHasher;
            this.webHostEnvironment = webHostEnvironment;
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
                thisViewModel.UserName = user.UserName;
                thisViewModel.ProfileImage = user.ProfilePictureUrl;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                UserList.Add(thisViewModel);
            }
            return View(UserList);
        }

        public async Task<IActionResult> Create()
        {
            await GetCountryState();
            return View();
        }
        [HttpPost, ActionName("GetStatesByCountryId")]
        public async Task<JsonResult> GetStatesByCountryId(string countryId) {
            int cId;
            var states = new List <State> ();
            if (!string.IsNullOrEmpty(countryId)) {
                cId = Convert.ToInt32(countryId);
                states = await context.States.Where(s => s.CountryId.Equals(cId)).ToListAsync();
            }
            return Json(states);
        }

        private async Task GetCountryState()
        {
            var countries = await context.Countries.ToListAsync();
            var states = new List<State>{};
            ViewData["countries"] = new SelectList(countries.OrderBy(s => s.CountryId), "CountryId", "CountryName");
            ViewData["states"] = new SelectList(states.OrderBy(s => s.StateId), "StateId", "StateName");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            user.Id = Guid.NewGuid().ToString();
            {
                if(user.ProfileImage != null && user.ProfileImage.Length >0 )
                {
                    string newFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(user.ProfileImage.FileName);
                    newFileName += fileExtension;
                    var upload = Path.Combine(webHostEnvironment.WebRootPath, "upload", newFileName);
                    user.ProfileImage.CopyTo(new FileStream(upload, FileMode.Create));
                    user.ProfilePictureUrl = newFileName;
                }

                IdentityResult result = await userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            await GetCountryState();
            return View(user);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            await GetCountryState();
            if (userId == null)
            {
                return NotFound();
            }

            var applicationUser = await userManager.FindByIdAsync(userId);
            if (applicationUser != null)
                return View(applicationUser);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(string id)
        {
            var user = await context.ApplicationUser.FirstOrDefaultAsync(a => a.Id == id);
            if(user is not null)
            {
                user.ProfilePictureUrl = null; 
                await context.SaveChangesAsync();
                return Ok(new { message = "succes", succeeded = true });
            }
            return NotFound("failed");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            {
                try
                {
                    //super admin should always have access to Roles
                    var user = await userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        user.PhoneNumber = applicationUser.PhoneNumber;
                        user.ProfilePictureUrl = applicationUser.ProfilePictureUrl;
                        user.FirstName = applicationUser.FirstName;
                        user.LastName = applicationUser.LastName;
                        user.Email = applicationUser.Email;
                        user.UserName = applicationUser.UserName;
                        user.Country = applicationUser.Country;
                        user.State= applicationUser.State;
                        var result  = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        Errors(result);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return Problem();
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        private async Task<List<string>> GetUserRoles(Models.ApplicationUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }
    }
}
