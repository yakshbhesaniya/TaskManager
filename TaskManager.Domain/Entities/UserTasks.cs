using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class UserTasks
    {
        [Key]
        public Guid TaskId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }  
        public string TaskDescription { get; set; }
        public DateTime TaskDateTime { get; set; }
        public DateTime AssignedTaskTime { get; set; }
        public DateTime TaskTotalTime { get; set; }
        public bool TaskStatus { get; set; }
        


    }
}
