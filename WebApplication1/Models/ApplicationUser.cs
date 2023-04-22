using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        public string profilePictureUrl { get; set; } = "/img/user.jpg";
        public bool isSuperAdmin { get; set; } = false;
        public byte[]? ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isInRoleHomeIndex { get; set; }
        public bool isInRoleHomeAbout { get; set; }
        public bool isInRoleHomeContact { get; set; }
        public bool isInRoleApplicationUser { get; set; }

        [Display(Name = "Home Roles")]
        public bool HomeRole { get; set; } = false;

        [Display(Name = "Roles")]
        public bool ApplicationUserRole { get; set; } = false;
    }
}
