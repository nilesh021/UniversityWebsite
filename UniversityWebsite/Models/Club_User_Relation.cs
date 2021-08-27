using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Club_User_Relation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Relation_Id { get; set; }

        [Required]
        [ForeignKey("Club")]
        public int Club_Id { get; set; }
        public Club Club { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Joined Date")]
        public DateTime Joined_Date { get; set; }
    }
}