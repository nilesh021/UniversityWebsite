using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityWebsite.Models
{
    public class CreateHelpTicketViewModel
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Issue")]
        public string Issue { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        

     



    }
}