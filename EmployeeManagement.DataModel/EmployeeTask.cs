using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DataModel
{
    public class EmployeeTask
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        [StringLength(50)]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}
