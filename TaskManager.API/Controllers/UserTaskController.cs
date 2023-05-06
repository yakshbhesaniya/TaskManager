using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeTask
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTasks>>> GetTaskDetails()
        {
          if (_context.TaskDetails == null)
          {
              return NotFound();
          }
            
            var tasks = await _context.TaskDetails.ToListAsync();

            return Ok(tasks.Select(u => new TaskDTO
            {
                TaskId = u.TaskId,
                TaskDescription = u.TaskDescription,
                TaskDateTime = u.TaskDateTime,
                TaskStatus = u.TaskStatus,
                UserId = u.UserId,
                AssignedTaskTime= u.AssignedTaskTime,
                TaskTotalTime= u.TaskTotalTime,
            })); ;
        }

        // GET: api/EmployeeTask/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTasks>> GetUserTasks(Guid id)
        {
          if (_context.TaskDetails == null)
          {
              return NotFound();
          }
            var employeeTasks = await _context.TaskDetails.FindAsync(id);

            if (employeeTasks == null)
            {
                return NotFound();
            }

            return employeeTasks;
        }

        // PUT: api/EmployeeTask/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTasks(Guid id, UserTasks userTasks)
        {
            if (id != userTasks.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(userTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeeTask
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTasks>> PostUserTasks(UserTasks userTasks)
        {
          if (_context.TaskDetails == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TaskDetails'  is null.");
          }
            _context.TaskDetails.Add(userTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTasks", new { id = userTasks.TaskId }, userTasks);
        }

        // DELETE: api/EmployeeTask/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTasks(Guid id)
        {
            if (_context.TaskDetails == null)
            {
                return NotFound();
            }
            var employeeTasks = await _context.TaskDetails.FindAsync(id);
            if (employeeTasks == null)
            {
                return NotFound();
            }

            _context.TaskDetails.Remove(employeeTasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTasksExists(Guid id)
        {
            return (_context.TaskDetails?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
