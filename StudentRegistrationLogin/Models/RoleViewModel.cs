using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {

        }
        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
            IsActive = role.IsActive;
            UpdatedBy = role.UpdatedBy;
            CreatedDateUtc = role.CreatedDateUtc;
            UpdatedDateUtc = role.UpdatedDateUtc;
        }

        public string Id { get; set; }
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role required")]
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
    }
}