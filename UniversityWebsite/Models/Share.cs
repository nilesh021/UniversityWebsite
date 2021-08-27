using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class Share
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Share_Id { get; set; }

        public int Shared_To_Id { get; set; }

        [Display(Name ="Type Of Item")]
        public string Shared_Type { get; set; }

        [Display(Name = "Item Id")]
        public int Shared_Type_Id { get; set; }

        [Display(Name ="Title")]
        public string Shared_Item_Title { get; set; }

        public int Shared_By_Id { get; set; }

        public bool Seen { get; set; }

        [ForeignKey("Shared_To_Id")]
        public virtual User Receiver { get; set; }

        [ForeignKey("Shared_By_Id")]
        public virtual User Sender { get; set; }
    }
}