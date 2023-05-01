using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }

    }
}
