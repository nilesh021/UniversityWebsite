using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace UniversityWebsite.Models
{
    public class Volunteer
    {
        [Key]
        public int Volunteer_Id { get; set; }

        public int User_Id { get; set; }

        public int Service_Id { get; set; }

        public virtual Service Service { get; set; }

        public virtual User User { get; set; }
    }
}