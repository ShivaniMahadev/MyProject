using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    [Table("Specialty")]
    public class Specialty
    {
        [Key]
        public int SpecialtyID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Specialty required")]
        [Display(Name = "Specialty Name")]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        public string IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
    }
}