using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Domain.Entities;

namespace TaskManager.API.DTOs
{
    public class ModulesDTO
    {

        public Guid ModuleId { get; set; }
        public UserTasks Tasks { get; set; }
        public string ModuleDescription { get; set; }
        public DateTime ModuleDateTime { get; set; }
        public DateTime ModuleTotalTime { get; set; }
        public bool ModuleStatus { get; set; }

    }
}
