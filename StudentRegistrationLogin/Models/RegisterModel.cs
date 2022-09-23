using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {

        }
        public RegisterModel(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.PasswordHash;
            ConfirmPassword = user.PasswordHash;
            FirstName = user.FirstName;
            LastName = user.LastName;
            DOB = user.DOB;
            ImagePath = user.ImagePath;
            FullName = user.FullName;
            StPrefix = user.PrefixName;
            UpdatedBy = user.Id;
            UpdatedDateUtc = DateTime.UtcNow;
            IsActive = user.IsActive;
        }
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string LastName { get; set; }          
        public string RoleName { get; set; }
        public DateTime? DOB { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name required")]
        public string FullName { get; set; }
        public string StPrefix { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public string IsActive { get; set; }
    }
}