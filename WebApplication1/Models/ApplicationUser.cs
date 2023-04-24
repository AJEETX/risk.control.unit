using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class ApplicationUser : IdentityUser<Guid>
    {
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public string? ProfilePictureUrl { get; set; }
        public bool isSuperAdmin { get; set; } = false;
        public byte[]? ProfilePicture { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile ProfileImage { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string? StateId { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        [NotMapped]
        public string? CountryId { get; set; }
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
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string code, string name)
        {
            Name = name;
            Code = code;
        }
        public string Code { get; set; }
    }
}
