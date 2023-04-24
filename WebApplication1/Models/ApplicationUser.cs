﻿using Microsoft.AspNetCore.Identity;
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
        [Required]
        [Display(Name = "State name")]
        public string StateId { get; set; }
        [Display(Name = "State name")]
        public State State { get; set; }
        [Required]
        [Display(Name = "Country name")]
        public string CountryId { get; set; }
        [Display(Name = "Country name")]
        public Country Country { get; set; }
        [Required]
        public string? Password { get; set; }
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
