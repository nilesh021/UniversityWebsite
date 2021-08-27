using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace UniversityWebsite.Models
{    public class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Volunteers = new HashSet<Volunteer>();
        }

        [Key]
        [Display(Name = "Service Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Service_Id { get; set; }

        [Display(Name="Admin_Id")]
        public int User_Id { get; set; }

       

        [Required]
        [Display(Name="Service Name")]
        [StringLength(20)]
        public string Service_Name { get; set; }

        [Required]
        [Display(Name = "Service Description")]
        [StringLength(100)]
        public string Service_Description { get; set; }

        [Display(Name = "Required Volunteers")]
        public int Required_Volunteer { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }

        [Display(Name = "Volunteers Participated")]
        public int? Participated_Volunteer { get; set; } = 0;

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}