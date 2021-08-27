using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityWebsite.Models
{
    public class OtherMemberViewModelcs
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string EventName { get; set; }

    }
}