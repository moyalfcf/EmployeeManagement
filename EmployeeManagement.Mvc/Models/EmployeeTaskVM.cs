using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Mvc.Models
{
    public class EmployeeTaskVM
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        [StringLength(50)]
        public string TaskName { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        public DateTime Deadline { get; set; }

        public SelectList Employees { get; set; }
    }
}
