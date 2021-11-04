using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Mvc.Models
{
    public class EmployeeTaskVM
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }

        public SelectList Employees { get; set; }
    }
}
