using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities;

namespace TaskManager.API.DTOs
{
    public class TaskDTO
    {
       
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDateTime { get; set; }
        public bool TaskStatus { get; set; }
    }
}
