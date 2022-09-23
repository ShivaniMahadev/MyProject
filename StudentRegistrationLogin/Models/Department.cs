using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Department required")]
        [Display(Name = "Department Name")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public string IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
    }
}