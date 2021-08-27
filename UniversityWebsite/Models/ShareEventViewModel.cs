using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class ShareEventViewModel
    {
        [ForeignKey("Event")]
        public int Event_Id { get; set; }
        public Event Event { get; set; }

        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
    }
}