using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class ApplicationUser : IdentityUser
    {
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public string ProfilePictureUrl { get; set; } = "/img/user.jpg";
        public bool isSuperAdmin { get; set; } = false;
        public byte[]? ProfilePicture { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ProfileImage { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string? Password { get; set; }
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
