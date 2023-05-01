using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; } 
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeUsername { get; set; }
        public string EmployeePassword { get; set; }
    }
}
