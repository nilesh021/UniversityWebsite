using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Idea_User_Relation
    {
   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idea_Relation_Id { get; set; }

        [Required]
        [ForeignKey("Idea")]
        public int Idea_Id { get; set; }
        public Idea Idea { get; set; }

        [Required]
        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }

        [Range(-1,1)]
        public int Vote { get; set; }

        public string Comment { get; set; }

    }
}