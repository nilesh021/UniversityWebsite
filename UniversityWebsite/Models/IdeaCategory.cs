using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniversityWebsite.Models
{
    public class IdeaCategory
    {
        [Key]
        public int Category_Id { get; set; }

        [Display(Name="Category Name")]
        public string Name { get; set; }
    }
}