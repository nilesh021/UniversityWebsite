using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Help
    {
        [Key]
       
        public int HelpId { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public int User_Id { get; set; }

        [Required]
        [Display(Name = "Issue")]
        public string Issue { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date of Ticket")]
        public DateTime DOT { get; set; }

        [Display(Name = "Admin Resolution")]
        public string AdminResolution { get; set; }
    }
}