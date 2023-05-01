using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> AdminDetails { get; set; }
        public DbSet<Employee> EmployeeDetails { get; set; }
        public DbSet<EmployeeTasks> TaskDetails { get; set; }



    }
}
