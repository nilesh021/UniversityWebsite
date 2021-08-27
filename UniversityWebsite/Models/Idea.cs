using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Idea
    {
        [Key]
        public int Idea_Id { get; set; }


        [Required]
        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Idea_Content { get; set; }

        [Required]
        [ForeignKey("Club")]
        public int Club_Id { get; set; }
        public Club Club { get; set; }

        [Required]
        [ForeignKey("IdeaCategory")]
        public int Category_Id { get; set; }
        public IdeaCategory IdeaCategory { get; set; }

        [Required]
        public int Votes { get; set; }

        [Required]
        [Display(Name ="Posted on")]
        [DataType(DataType.Date)]
        public DateTime Posted_Date { get; set; }
    }
}