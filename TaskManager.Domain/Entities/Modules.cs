using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Modules
    {
        [Key]
        public Guid ModuleId { get; set; }
        public UserTasks Tasks { get; set; }
        public string ModuleDescription { get; set; }
        public DateTime ModuleDateTime { get; set; }
        public DateTime ModuleTotalTime { get; set; }
        public bool ModuleStatus { get; set; }
            
    }
}
