using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    [Table("DoctorSpecialty")]
    public class DoctorSpeciality
    {
        [Key]
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public int SpecialityId { get; set; }
        
        [Display(Name = "Doctor Name")]
        [StringLength(50)]
        public string DoctorName { get; set; }
        
        [Display(Name = "Specialty Name")]
        [StringLength(100)]
        public string SpecialityName { get; set; }

        [NotMapped]
        public IEnumerable<Specialty> SpecialityCollection { get; set; }

        [NotMapped]
        public string[] SelectArray { get; set; }

        public string IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
    }
}