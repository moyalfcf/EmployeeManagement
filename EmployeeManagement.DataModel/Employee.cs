using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.DataModel
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public ICollection<EmployeeTask> Tasks { get; set; }

        [NotMapped]
        public string FullName { get { return LastName + ", " + FirstName; } }
    }
}
