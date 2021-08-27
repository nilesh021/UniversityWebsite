using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class News
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int News_Id { get; set; }

        [Required]
        [Display(Name ="News name")]
        public string News_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }

        [Required]
        public string Category { get; set; }
    }
}