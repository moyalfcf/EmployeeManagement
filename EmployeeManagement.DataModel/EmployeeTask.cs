using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DataModel
{
    public class EmployeeTask
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}
