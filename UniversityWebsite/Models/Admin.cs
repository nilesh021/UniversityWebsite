using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Security Question 1: Favourite Book")]
        public string A1 { get; set; }

        [Required]
        [Display(Name = "Security Question 2: City Of Birth")]
        public string A2 { get; set; }

        [Required]
        [Display(Name = "Security Question 3: First School")]
        public string A3 { get; set; }
    }
}