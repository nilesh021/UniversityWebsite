using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWebsite.Models
{
    public class CreateGrievanceViewModel
    {
        [Key]
        public int Grivanance_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }


        [Required]
        [StringLength(150)]
        public string Topic { get; set; }

        [Required]
        [StringLength(150)]
        public string Sub_Topic { get; set; }

        [Required]
        [StringLength(300)]
        public string Details { get; set; }
    }
}