using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebsite.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AddEventReactionViewModel
    {
            [Key]
            public int Id { get; set; }

              public int Event_Id { get; set; }
        [MaxLength(200)]
            public string Comment { get; set; }

    }
}