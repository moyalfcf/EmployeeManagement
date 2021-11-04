using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DataModel
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Hired Date")]
        public DateTime HiredDate { get; set; }
        public ICollection<EmployeeTask> Tasks { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName { get { return LastName + ", " + FirstName; } }
    }
}
