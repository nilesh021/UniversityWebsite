using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityWebsite.Models;


namespace UniversityWebsite.Models
{
    public class NewHelpViewModel
    {
        public IEnumerable<Help> HelpModelRecord { get; set; }

        public Help helpModel { get; set; }
    }
}