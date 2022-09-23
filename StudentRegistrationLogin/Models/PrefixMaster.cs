using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationLogin.Models
{
    [Table("PrefixMaster")]
    public class PrefixMaster
    {
        [Key]
        public int PrefixID { get; set; }

        [Display(Name = "Prefix Name")]
        [StringLength(10)]
        public string PrefixName { get; set; }
    }
}