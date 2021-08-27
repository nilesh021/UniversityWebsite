using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace UniversityWebsite.Models
{
    public class Events_User_Relation
    {
        public int Id { get; set; }

        public int Event_Id { get; set; }

        public int User_Id { get; set; }

        public int? Like_Dislike { get; set; } = 0;

        public int? Interest { get; set; } = 0;

        public string Comment { get; set; }

        public int? Attendance { get; set; }

        public virtual Event Event { get; set; }
    }
}