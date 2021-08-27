using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Club
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Club_Id { get; set; }

        [Required]
        [Display(Name = "Club Name")]
        public string Club_Name { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Maximum Members")]
        public int Max_Member { get; set; }
    }
}