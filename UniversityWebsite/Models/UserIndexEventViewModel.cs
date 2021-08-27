using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityWebsite.Models
{
    public class UserIndexEventViewModel
    {

        [Key]
        public int Id { get; set; }

        public int Event_Id { get; set; }

        public int User_Id { get; set; }

        public int? Like_Dislike { get; set; } = 0;

        public int? Interest { get; set; } = 0;

        public string Comment { get; set; } = "";

        public int? Attendance { get; set; } = 0;

       
        public string Event_Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime End_Date { get; set; }

       
        public string Category { get; set; }

        public int Participated_User { get; set; }


    }
}