using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class EmployeeTasks
    {
        [Key]
        public Guid TaskId { get; set; }
        [ForeignKey("Education")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }  
        public string TaskDescription { get; set; }
        public DateTime TaskDateTime { get; set; } 
        public bool TaskStatus { get; set; }
        
    }
}
